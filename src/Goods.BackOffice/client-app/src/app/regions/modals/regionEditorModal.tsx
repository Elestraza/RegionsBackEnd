import React, { useEffect, useState } from 'react';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { FederalRegions } from '../../../domain/federalRegions/federalRegions';
import { RegionsProvider } from '../../../domain/regions/regionsProvider';
import { RegionsBlank } from '../../../domain/regions/regionsBlank';
import { Regions } from '../../../domain/regions/regions';
import { FederalRegionsProvider } from '../../../domain/federalRegions/federalRegionsProvider';
interface Props {
	regionId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function RegionEditorModal(props: Props) {
	const [blank, setRegionBlank] = useState<RegionsBlank>(RegionsBlank.getEmpty());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	const [federalRegions, setFederalRegions] = useState<FederalRegions[]>([]);
	const [isLoadingFederalRegions, setIsLoadingFederalRegions] = useState(false);

	useEffect(() => {
		if (!props.isOpen) return;

		async function loadRegionBlank() {
			let blank: RegionsBlank | null = null;

			if (props.regionId != null) {
				const region: Regions | null = await RegionsProvider.getRegionById(props.regionId);
				if (region == null) throw 'Region is null';

				blank = RegionsBlank.getFromRegion(region);
			}

			setRegionBlank(blank ?? RegionsBlank.getEmpty());
		}

		async function loadFederalRegions() {
			setIsLoadingFederalRegions(true);
			try {
				const page = await FederalRegionsProvider.getFederalRegionsPage(0, 1000);
				
				const federalRegionsArray = 
					(page as any).values || 
					(page as any).items || 
					(page as any).data || 
					(page as any).content || 
					(page as any).elements || 
					[];

				setFederalRegions(federalRegionsArray);
			} catch (e) {
				console.error('Ошибка загрузки регионов:', e);
				setErrorMessage('Не удалось загрузить список регионов');
			} finally {
				setIsLoadingFederalRegions(false);
			}
		}		

		loadRegionBlank();
		loadFederalRegions();

		return () => {
			setRegionBlank(RegionsBlank.getEmpty());
			setErrorMessage(null);
		};
	}, [props.isOpen, props.regionId]);

	async function saveRegion() {
		const result = await RegionsProvider.saveRegion(blank);
		if (!result.isSuccess) {
			setErrorMessage(result.errorsAsString);
			return;
		}

		props.onClose(true);
	}

	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор регионов</Modal.Header>
				<Modal.Body
					sx={{
						maxWidth: '800px',
						minWidth: '600px',
						display: 'flex',
						flexDirection: 'column',
						gap: '12px'
					}}>		
					<Input 
						variant='select'
						title='Выберите регион'
						options={federalRegions.map(r => r)}
						getOptionLabel={(option) => {
							const federalRegion = federalRegions.find(r => r.id === option.id);
							return federalRegion ? `${federalRegion.name}` : '';
						}}
						isOptionEqualToValue={(a, b) => a === b}
						value={blank.federalRegion}
						onChange={(federalRegion: FederalRegions | null) => { setRegionBlank((blank) => ({ ...blank, federalRegion: federalRegion})); }}
						disabled={isLoadingFederalRegions} 
						required
					/>
					<Input
						variant='text'
						title='Введите название региона'
						value={blank.name}
						onChange={(name) => setRegionBlank((nlank) => ({ ...nlank, name }))}
						required
					/>
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => saveRegion()} />
				</Modal.Footer>
			</Modal>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</>
	);
}
