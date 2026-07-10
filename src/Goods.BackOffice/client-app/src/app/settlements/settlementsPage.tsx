import {
	Container,
	Paper,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
	Typography
} from '@mui/material';
import React, { useEffect, useState } from 'react';

import { Settlements } from '../../domain/settlements/settlements';
import { SettlementsTypes } from '../../domain/settlements/settlementsTypes';
import { SettlementsProvider } from '../../domain/settlements/settlementsProvider';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { SettlementEditorModal } from './modals/settlementEditorModal';

type SettlementEditorModalState = {
	settlementId: string | null;
	isOpen: boolean;
};

interface RemoveSettlementConfirmModalState extends ConfirmModalState {
	settlementId: string | null;
}

interface HistoryValueOfSettlementConfirmModalState extends ConfirmModalState {
	settlementId: string | null;
}

export function SettlementsPage() {
	const [settlements, setSettlement] = useState<Settlements[]>([]);
	const [pagination, setPagination] = useState<Pagination>(Pagination.default);

	const [settlementEditorModalState, setSettlementEditorModalState] = useState<SettlementEditorModalState>({
		settlementId: null,
		isOpen: false
	});
	const [removeSettlementConfirmModalState, setRemoveSettlementConfirmModalState] =
		useState<RemoveSettlementConfirmModalState>({ settlementId: null, ...ConfirmModalState.getClosed() });
	const [historyValueModalState, setHistoryValueModalState] =
		useState<HistoryValueOfSettlementConfirmModalState>({ settlementId: null, ...ConfirmModalState.getClosed() });

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadSettlementsPage({ ...pagination });
	}, []);

	async function loadSettlementsPage(newPagination: Pagination) {
		const page = await SettlementsProvider.getSettlementsPage(newPagination.page, newPagination.pageSize);

		setSettlement(page.values);
		setPagination((pagination) => ({
			...pagination,
			page: newPagination.page,
			pageSize: newPagination.pageSize,
			totalRows: page.totalRows
		}));
	}
	
	function openSettlementEditorModal(settlementId?: string) {
		setSettlementEditorModalState({ settlementId: settlementId ?? null, isOpen: true });
	}

	function closeSettlementEditorModal(isEdited: boolean) {
		if (isEdited) loadSettlementsPage({ ...pagination, page: 1 });
		setSettlementEditorModalState({ settlementId: null, isOpen: false });
	}

	function openRemoveSettlementConfirmModal(settlementId: string, name: string) {
		setRemoveSettlementConfirmModalState({
			settlementId,
			...ConfirmModalState.getOpen(`Вы действительно хотите удалить населенный пункт "${name}"`)
		});
	}
	


/*
*
*		ВОЗРАСТ СТАНОВЛЕНИЯ НП ИСТОРИЧЕСКИ ЦЕННЫМ ЗАВИСИТ ОТ ФЕДЕРАЛЬНОГО РЕГИОНА 
*		СЕРВЕРНАЯ ЛОГИКА
*/
	async function openHistoricalValueModal(settlementId: string) {		
		const response = await fetch(`/settlements/settlement-history-value/get-by-id?id=${settlementId}`)
		.then(response => response.text())
		.then((response) => {
			setHistoryValueModalState({
				settlementId, ...ConfirmModalState.getOpen(response)
			});
			console.log("RESPONCE: ", response);
		})
		.catch(err => console.log(err))
		
	}

	async function closeRemoveSettlementConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeSettlementConfirmModalState.settlementId == null) throw 'Cannot remove product with SettlementId = null';

			const result = await SettlementsProvider.removeSettlement(removeSettlementConfirmModalState.settlementId);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadSettlementsPage({ ...pagination, page: 1 });
		}

		setRemoveSettlementConfirmModalState({ settlementId: null, ...ConfirmModalState.getClosed() });
	}

	async function closeHistoryValueModal(isConfirmed: boolean) {
		if (isConfirmed) {
			loadSettlementsPage({ ...pagination, page: 1 });
		}

		setHistoryValueModalState({settlementId: null, ...ConfirmModalState.getClosed()});
	}

	return (
		<Container
			sx={{ height: '100%', display: 'flex', flexDirection: 'column', gap: '12px' }}
			maxWidth={false}
			disableGutters>
			<Paper
				elevation={3}
				sx={{
					display: 'flex',
					alignItems: 'center',
					justifyContent: 'space-between',
					paddingX: '12px',
					paddingY: '6px'
				}}>
				<Typography variant='h6'>Населенные пункты</Typography>
				<Button variant='add' title='Создать' onClick={() => openSettlementEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>Название</TableCell>
								<TableCell>Тип населенного пункта</TableCell>
								<TableCell>Регион</TableCell>
								<TableCell>Население</TableCell>
								<TableCell>Год основания</TableCell>
								<TableCell>Средняя стоимость отеля</TableCell>
								<TableCell>Статус героя</TableCell>
								<TableCell>Управление</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{
								settlements.length === 0 &&
								<TableRow>
									<TableCell colSpan={5}>Пусто</TableCell>
								</TableRow>
							}
							{
								settlements.map(settlement => (
									<TableRow key={`product__${settlement.id}`}>
										<TableCell width='15%'>{settlement.name}</TableCell>
										<TableCell width='15%'>
											{SettlementsTypes.getDisplayName(settlement.type)}
										</TableCell>
										<TableCell width='15%'>{settlement.region.name}</TableCell>
										<TableCell width='5%'>{settlement.population}</TableCell>
										<TableCell width='10%'>{settlement.foundationYear}</TableCell>
										<TableCell width='15%'>{settlement.averageHotelCost}</TableCell>
										<TableCell width='10%'>{`${settlement.isHero}`}</TableCell>
										<TableCell>
											<Button
												type='icon'
												variant='edit'
												size='small'
												onClick={() => openSettlementEditorModal(settlement.id)} />
											<Button
												type='icon'
												variant='remove'
												size='small'
												onClick={() => openRemoveSettlementConfirmModal(settlement.id, settlement.name)} />
											<Button
												type='icon'
												variant='confirm'
												size='small'
												onClick={() => openHistoricalValueModal(settlement.id)} />
										</TableCell>
									</TableRow>
								))
							}
						</TableBody>
					</Table>
				</TableContainer>

				<TablePagination
					countInPageOptions={Pagination.pageSizeOptions}
					page={pagination.page}
					countInPage={pagination.pageSize}
					totalRows={pagination.totalRows}
					changePage={page => loadSettlementsPage({ ...pagination, page })}
					changeCountInPage={pageSize => loadSettlementsPage({ ...pagination, pageSize })}
				/>
			</Paper>

			<SettlementEditorModal
				isOpen={settlementEditorModalState.isOpen}
				settlementId={settlementEditorModalState.settlementId}
				onClose={closeSettlementEditorModal}
			/>

			<ConfirmModal
				title={removeSettlementConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveSettlementConfirmModal(isConfirmed)}
				isOpen={removeSettlementConfirmModalState.isOpen}
			/>

			<ConfirmModal
				title={historyValueModalState.title}
				onClose={(isConfirmed) => closeHistoryValueModal(isConfirmed)}
				isOpen={historyValueModalState.isOpen}
			/>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</Container>
	);
}
