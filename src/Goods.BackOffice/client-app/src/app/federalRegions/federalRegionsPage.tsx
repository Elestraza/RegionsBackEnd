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
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { FederalRegionsEditorModal } from './modals/federalRegionsEditorModal';
import { FederalRegionsProvider } from '../../domain/federalRegions/federalRegionsProvider';
import { FederalRegions } from '../../domain/federalRegions/federalRegions';


type federalRegionsEditorModalState = {
	federalRegionId: string | null;
	isOpen: boolean;
};

interface RemoveFederalRegionConfirmModalState extends ConfirmModalState {
	federalRegionId: string | null;
}

export function FederalRegionsPage() {
	const [federalRegions, setFederalRegions] = useState<FederalRegions[]>([]);
	const [pagination, setPagination] = useState<Pagination>(Pagination.default);

	const [federalRegionsEditorModalState, setFederalRegionsEditorModalState] = useState<federalRegionsEditorModalState>({
		federalRegionId: null,
		isOpen: false
	});
	const [removeFederalRegionConfirmModalState, setRemoveFederalRegionConfirmModalState] =
		useState<RemoveFederalRegionConfirmModalState>({ federalRegionId: null, ...ConfirmModalState.getClosed() });

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadFederalRegionsPage({ ...pagination });
	}, []);

	async function loadFederalRegionsPage(newPagination: Pagination) {
		const federalRegionsPage = await FederalRegionsProvider.getFederalRegionsPage(newPagination.page, newPagination.pageSize);

		setFederalRegions(federalRegionsPage.values);
		setPagination((pagination) => ({
			...pagination,
			page: newPagination.page,
			pageSize: newPagination.pageSize,
			totalRows: federalRegionsPage.totalRows
		}));
	}

	function openFederalRegionsEditorModal(federalRegionID?: string) {
		setFederalRegionsEditorModalState({ federalRegionId: federalRegionID ?? null, isOpen: true });
	}

	function closeFederalRegionsEditorModal(isEdited: boolean) {
		if (isEdited) loadFederalRegionsPage({ ...pagination, page: 1 });
		setFederalRegionsEditorModalState({ federalRegionId: null, isOpen: false });
	}

	function openRemoveFederalRegionsConfirmModal(federalRegionId: string, name: string) {
		setRemoveFederalRegionConfirmModalState({
			federalRegionId,
			...ConfirmModalState.getOpen(`Вы действительно хотите удалить федеральный регион "${name}"`)
		});
	}

	async function closeRemoveFederalRegionsConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeFederalRegionConfirmModalState.federalRegionId == null) throw 'Cannot remove CarCode with ID = null';

			const result = await FederalRegionsProvider.removeFederalRegion(removeFederalRegionConfirmModalState.federalRegionId);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadFederalRegionsPage({ ...pagination, page: 1 });
		}

		setRemoveFederalRegionConfirmModalState({ federalRegionId: null, ...ConfirmModalState.getClosed() });
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
				<Typography variant='h6'>Федеральные регоины</Typography>
				<Button variant='add' title='Создать' onClick={() => openFederalRegionsEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>Название</TableCell>
								<TableCell>Возраст исторической ценности</TableCell>
								<TableCell>Управление</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{
								federalRegions.length === 0 &&
								<TableRow>
									<TableCell colSpan={5}>Пусто</TableCell>
								</TableRow>
							}
							{
								federalRegions.map(federalRegions => (
									<TableRow key={`product__${federalRegions.id}`}>
										<TableCell width='20%'>{federalRegions.name}</TableCell>
										<TableCell width='40%'>{federalRegions.historicalValueAge}</TableCell>
										<TableCell>
											<Button
												type='icon'
												variant='edit'
												size='small'
												onClick={() => openFederalRegionsEditorModal(federalRegions.id)} />
											<Button
												type='icon'
												variant='remove'
												size='small'
												onClick={() => openRemoveFederalRegionsConfirmModal(federalRegions.id, federalRegions.name)} />
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
					changePage={page => loadFederalRegionsPage({ ...pagination, page })}
					changeCountInPage={pageSize => loadFederalRegionsPage({ ...pagination, pageSize })}
				/>
			</Paper>

			<FederalRegionsEditorModal
				isOpen={federalRegionsEditorModalState.isOpen}
				federalRegionsId={federalRegionsEditorModalState.federalRegionId}
				onClose={closeFederalRegionsEditorModal}
			/>

			<ConfirmModal
				title={removeFederalRegionConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveFederalRegionsConfirmModal(isConfirmed)}
				isOpen={removeFederalRegionConfirmModalState.isOpen}
			/>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</Container>
	);
}
