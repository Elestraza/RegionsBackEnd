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
import { Regions } from '../../domain/regions/regions';
import { RegionsProvider } from '../../domain/regions/regionsProvider';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { RegionEditorModal } from './modals/regionEditorModal';

type RegionEditorModalState = {
	id: string | null;
	isOpen: boolean;
};

interface RemoveRegionConfirmModalState extends ConfirmModalState {
	id: string | null;
}

export function RegionsPage() {
	const [regions, setRegions] = useState<Regions[]>([]);
	const [pagination, setPagination] = useState<Pagination>(Pagination.default);

	const [regionEditorModalState, setRegionEditorModalState] = useState<RegionEditorModalState>({
		id: null,
		isOpen: false
	});
	const [removeRegionConfirmModalState, setRemoveRegionConfirmModalState] =
		useState<RemoveRegionConfirmModalState>({ id: null, ...ConfirmModalState.getClosed() });

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadRegionsPage({ ...pagination });
	}, []);

	async function loadRegionsPage(newPagination: Pagination) {
		const regionsPage = await RegionsProvider.getRegionsPage(newPagination.page, newPagination.pageSize);

		setRegions(regionsPage.values);
		setPagination((pagination) => ({
			...pagination,
			page: newPagination.page,
			pageSize: newPagination.pageSize,
			totalRows: regionsPage.totalRows
		}));
	}

	function openRegionsEditorModal(id?: string) {
		setRegionEditorModalState({ id: id ?? null, isOpen: true });
	}

	function closeRegionsEditorModal(isEdited: boolean) {
		if (isEdited) loadRegionsPage({ ...pagination, page: 1 });
		setRegionEditorModalState({ id: null, isOpen: false });
	}

	function openRemoveRegionConfirmModal(id: string, productName: string) {
		setRemoveRegionConfirmModalState({
			id,
			...ConfirmModalState.getOpen(`Вы действительно хотите удалить продукт "${productName}"`)
		});
	}

	async function closeRemoveRegionConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeRegionConfirmModalState.id == null) throw 'Cannot remove product with ProductId = null';

			const result = await RegionsProvider.removeRegion(removeRegionConfirmModalState.id);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadRegionsPage({ ...pagination, page: 1 });
		}

		setRemoveRegionConfirmModalState({ id: null, ...ConfirmModalState.getClosed() });
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
				<Typography variant='h6'>Регионы</Typography>
				<Button variant='add' title='Создать' onClick={() => openRegionsEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>Название</TableCell>
								<TableCell>Федеральный регион</TableCell>
								<TableCell>Управление</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{
								regions.length === 0 &&
								<TableRow>
									<TableCell colSpan={5}>Пусто</TableCell>
								</TableRow>
							}
							{
								regions.map(region => (
									<TableRow key={`product__${region.id}`}>

										<TableCell width='20%'>{region.name}</TableCell>
										<TableCell width='15%'>{region.federalRegion.name}</TableCell>
										<TableCell>
											<Button
												type='icon'
												variant='edit'
												size='small'
												onClick={() => openRegionsEditorModal(region.id)} />
											<Button
												type='icon'
												variant='remove'
												size='small'
												onClick={() => openRemoveRegionConfirmModal(region.id, region.name)} />
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
					changePage={page => loadRegionsPage({ ...pagination, page })}
					changeCountInPage={pageSize => loadRegionsPage({ ...pagination, pageSize })}
				/>
			</Paper>

			<RegionEditorModal
				isOpen={regionEditorModalState.isOpen}
				regionId={regionEditorModalState.id}
				onClose={closeRegionsEditorModal}
			/>

			<ConfirmModal
				title={removeRegionConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveRegionConfirmModal(isConfirmed)}
				isOpen={removeRegionConfirmModalState.isOpen}
			/>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</Container>
	);
}
