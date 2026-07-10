import React, { useEffect, useState } from 'react';
import { Settlements } from '../../../domain/settlements/settlements';
import { SettlementsBlank } from '../../../domain/settlements/settlementsBlank';
import { SettlementsTypes } from '../../../domain/settlements/settlementsTypes';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';
import { Enum } from '../../../tools/types/enum';
import { SettlementsProvider } from '../../../domain/settlements/settlementsProvider';
import { Regions } from '../../../domain/regions/regions';
import { RegionsProvider } from '../../../domain/regions/regionsProvider';
import { DateTimePicker } from 'react-datetime-picker'; // <DateTimePicker onChange={onChange} value={value} />
interface Props {
	settlementId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function SettlementEditorModal(props: Props) {
	const [blank, setSettlementBlank] = useState<SettlementsBlank>(SettlementsBlank.getEmpty());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	const [regions, setRegions] = useState<Regions[]>([]);
	const [isLoadingRegions, setIsLoadingRegions] = useState(false);

	useEffect(() => {
		console.log(`Стейт blank.isHero : ${blank.isHero}`);
	}, [blank.isHero]);
	useEffect(() => {
		if (!props.isOpen) return;

		async function loadSettlementBlank() {
			let blank: SettlementsBlank | null = null;

			if (props.settlementId != null) {
				const settlement: Settlements | null = await SettlementsProvider.getSettlementById(props.settlementId);
				if (settlement == null) throw 'Settlement is null';

				blank = SettlementsBlank.getFromSettlement(settlement);
				
			}
			
			setSettlementBlank(blank ?? SettlementsBlank.getEmpty());
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

		loadSettlementBlank();
		loadRegions();

		return () => {
			setSettlementBlank(SettlementsBlank.getEmpty());
			setErrorMessage(null);
		};
	}, [props.isOpen, props.settlementId]);

	async function saveSettlement() {
		const result = await SettlementsProvider.saveSettlements(blank);
		if (!result.isSuccess) {
			setErrorMessage(result.errorsAsString);
			return;
		}

		props.onClose(true);
	}


	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор населённых пунктов</Modal.Header>
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
						title='Выберите тип населенного пункта'
						options={Enum.getNumberValues<SettlementsTypes>(SettlementsTypes)}
						getOptionLabel={(option) => SettlementsTypes.getDisplayName(option)}
						isOptionEqualToValue={(a, b) => a === b}
						value={blank.type}
						onChange={(type) => setSettlementBlank((blank) => ({ ...blank, type }))}
						required
					/>
					<Input
						variant='text-area'
						title='Введите название'
						value={blank.name}
						onChange={(name) => setSettlementBlank((blank) => ({ ...blank, name }))}
						required
					/>
					<Input
						variant='number'
						title='Введите население'
						value={blank.population}
						onChange={(population) => setSettlementBlank((blank) => ({ ...blank, population }))}
						required
					/>
					<Input
						variant='number'
						title='Введите год основания'
						value={blank.foundationYear}
						onChange={(foundationYear) => setSettlementBlank((blank) => ({ ...blank, foundationYear }))}
						required
					/>
					<Input
						variant='number'
						title='Введите среднюю стоимость отеля за ночь'
						value={blank.averageHotelCost}
						onChange={(averageHotelCost) => setSettlementBlank((blank) => ({ ...blank, averageHotelCost }))}
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
						value={blank.region}
						onChange={(regions: Regions | null) => { setSettlementBlank((blank) => ({ ...blank, region: regions})); }}
						disabled={isLoadingRegions} 
						required
					/>
					<label style={{ display: 'flex', alignItems: 'center', gap: '8px', cursor: 'pointer' }}>
						<input 
							type="checkbox" 
							checked={blank.isHero ?? false}
							onChange={(e) => setSettlementBlank(prev => ({ ...prev, isHero: e.target.checked })) } 
						/>
						<span>Город имеет статус Герой-город</span>
					</label>
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => {
							if (blank.type !== 1 && blank.isHero === true) setErrorMessage("Данный населенный пункт не может иметь статус Города-героя.");	
							else saveSettlement()
						}} 
					/>
				</Modal.Footer>
			</Modal>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</>
	);
}
