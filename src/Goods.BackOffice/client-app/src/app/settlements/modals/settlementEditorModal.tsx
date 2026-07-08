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

interface Props {
	settlementId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

interface EditProps {
	settlementtype: SettlementsTypes | null,
	name: string | null,
	population: number | null,
	region: string | null,
	foundationdate: number | null,
	ishero: boolean | null,
	averagehotelcost: number | null
}

export function SettlementEditorModal(props: Props) {
	const [blank, setSettlementBlank] = useState<SettlementsBlank>(SettlementsBlank.getEmpty());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	const [regions, setRegions] = useState<Regions[]>([]);
	const [isLoadingRegions, setIsLoadingRegions] = useState(false);

	const [checked, setChecked] = useState<boolean>(true);
	
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
				const page = await RegionsProvider.getRegionsPage(1, 1000);
				
				const regionsArray = (page as any).items || (page as any).data || [];
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

	async function saveProduct() {
		const result = await SettlementsProvider.saveSettlements(blank);
		if (!result.isSuccess) {
			setErrorMessage(result.errorsAsString);
			return;
		}

		props.onClose(true);
	}
	const selectedRegion = regions.find(r => r.id === blank.region) ?? null;
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
						value={blank.settlementtype}
						onChange={(type) => setSettlementBlank((blank) => ({ ...blank, type }))}
						required
					/>
					<Input
						variant='text'
						title='Введите название'
						value={blank.name}
						onChange={(name) => setSettlementBlank((blank) => ({ ...blank, name }))}
						required
					/>
					<Input
						variant='number'
						title='Введите население'
						value={blank.population}
						onChange={(population) =>
							setSettlementBlank((blank) => ({ ...blank, population }))
						}
					/>
					<Input
						variant='number'
						title='Введите год основания'
						value={blank.foundationdate}
						onChange={(foundationDate) => setSettlementBlank((blank) => ({ ...blank, foundationDate }))}
						required
					/>
					<Input
						variant='number'
						title='Введите среднюю стоимость отеля за ночь'
						value={blank.averagehotelcost}
						onChange={(averageCost) => setSettlementBlank((blank) => ({ ...blank, averageCost }))}
						required
					/>
					<Input // ПРОВЕРИТЬ ЗАВТРА НА РАБОЧЕМ КОМПЕ
						variant='select'
						title='Выберите регион'
						options={regions} 
						getOptionLabel={(option: Regions) => option.name}
						isOptionEqualToValue={(option: Regions, value: Regions | null) => 
							value != null ? option.id === value.id : false
						}
						value={selectedRegion} 
						onChange={(selectedRegion: Regions | null) => { 
							setSettlementBlank(prev => ({ ...prev, region: selectedRegion ? selectedRegion.id : ''  })); 
						}}
						disabled={isLoadingRegions} 
						required
					/>
					<input name="Статус героя" type="checkbox" checked={checked} 
          				onChange={e => setChecked(!checked)} />
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => saveProduct()} />
				</Modal.Footer>
			</Modal>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</>
	);
}
