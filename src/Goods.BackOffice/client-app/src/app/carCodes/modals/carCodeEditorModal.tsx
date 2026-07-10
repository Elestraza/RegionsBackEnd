import React, { useEffect, useState } from 'react';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { CarCodes } from '../../../domain/carCodes/carCodes';
import { CarCodesBlank } from '../../../domain/carCodes/carCodesBlank';
import { CarCodesProvider } from '../../../domain/carCodes/carCodesProvider';
import { Regions } from '../../../domain/regions/regions';
import { RegionsProvider } from '../../../domain/regions/regionsProvider';

interface Props {
	carCodeId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function CarCodeEditorModal(props: Props) {
	const [blank, setCarCodesBlank] = useState<CarCodesBlank>(CarCodesBlank.getEmpty());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);
	
	const [regions, setRegions] = useState<Regions[]>([]);
	const [isLoadingRegions, setIsLoadingRegions] = useState(false);

	useEffect(() => {
		if (!props.isOpen) return;

		async function loadCarCodesBlank() {
			let blank: CarCodesBlank | null = null;

			if (props.carCodeId != null) {
				const carCode: CarCodes | null = await CarCodesProvider.getCarCodeById(props.carCodeId);
				if (carCode == null) throw 'Car code is null';

				blank = CarCodesBlank.getFromCarCode(carCode);
			}

			setCarCodesBlank(blank ?? CarCodesBlank.getEmpty());
		}

		async function loadRegions() {
			setIsLoadingRegions(true);
			try {
				const page = await RegionsProvider.getRegionsPage(0, 1000);
				
				const regionsArray = 
					(page as any).values || 
					(page as any).items || 
					(page as any).data || 
					(page as any).content || 
					(page as any).elements || 
					[];
		
				setRegions(regionsArray);
			} catch (e) {
				console.error('Ошибка загрузки регионов:', e);
				setErrorMessage('Не удалось загрузить список регионов');
			} finally {
				setIsLoadingRegions(false);
			}
		}

		loadCarCodesBlank();
		loadRegions();

		return () => {
			setCarCodesBlank(CarCodesBlank.getEmpty());
			setErrorMessage(null);
		};
	}, [props.isOpen, props.carCodeId]);

	async function saveCarCode() {
		const result = await CarCodesProvider.saveCarCode(blank);
		if (!result.isSuccess) {
			setErrorMessage(result.errorsAsString);
			return;
		}

		props.onClose(true);
	}

	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор автомобильных кодов</Modal.Header>
				<Modal.Body
					sx={{
						maxWidth: '800px',
						minWidth: '600px',
						display: 'flex',
						flexDirection: 'column',
						gap: '12px'
					}}>
					<Input
						variant='text'
						title='Введите код'
						value={blank.code}
						onChange={(code) => setCarCodesBlank(prev => ({ ...prev, code }))}
						required
					/>
					<Input 
						variant='select'
						title='Выберите регион'
						options={regions.map(r => r)}
						getOptionLabel={(option) => {
							const region = regions.find(r => r.id === option.id);
							return region ? `${region.name}` : '';
						}}
						isOptionEqualToValue={(a, b) => a === b}
						value={blank.regions}
						onChange={(region: Regions | null) => { setCarCodesBlank((blank) => ({ ...blank, regions: region})); }}
						disabled={isLoadingRegions} 
						required
					/>
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => {
						if ( (/^\d+$/.test(blank.code)) === false) setErrorMessage("Автомобильный код может содержать только цифры.");
						else saveCarCode()
						}} /> 
				</Modal.Footer>
			</Modal>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</>
	);
}