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
import { ProductsProvider } from '../../domain/products/productsProvider';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { ProductEditorModal } from './modals/productEditorModal';
import { CarCodesProvider } from '../../domain/carCodes/carCodesProvider';
import { CarCodes } from '../../domain/carCodes/CarCodes';

type carCodeEditorModalState = {
	carCodeId: string | null;
	isOpen: boolean;
};

interface RemoveCarCodeConfirmModalState extends ConfirmModalState {
	carCodeId: string | null;
}

export function CarCodesPage() {
	const [carCodes, setCarCodes] = useState<CarCodes[]>([]);
	const [pagination, setPagination] = useState<Pagination>(Pagination.default);

	const [carCodesEditorModalState, setCarCodesEditorModalState] = useState<carCodeEditorModalState>({
		carCodeId: null,
		isOpen: false
	});
	const [removeCarCodeConfirmModalState, setRemoveCarCodeConfirmModalState] =
		useState<RemoveCarCodeConfirmModalState>({ carCodeId: null, ...ConfirmModalState.getClosed() });

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadCarCodesPage({ ...pagination });
	}, []);

	async function loadCarCodesPage(newPagination: Pagination) {
		const carCodesPage = await CarCodesProvider.getCarCodesPage(newPagination.page, newPagination.pageSize);

		setCarCodes(carCodesPage.values);
		setPagination((pagination) => ({
			...pagination,
			page: newPagination.page,
			pageSize: newPagination.pageSize,
			totalRows: carCodesPage.totalRows
		}));
	}

	function openCarCodesEditorModal(carCodeId?: string) {
		setCarCodesEditorModalState({ carCodeId: carCodeId ?? null, isOpen: true });
	}

	function closeCarCodesEditorModal(isEdited: boolean) {
		if (isEdited) loadCarCodesPage({ ...pagination, page: 1 });
		setCarCodesEditorModalState({ carCodeId: null, isOpen: false });
	}

	function openRemoveCarCodesConfirmModal(carCodeId: string, code: number) {
		setRemoveCarCodeConfirmModalState({
			carCodeId,
			...ConfirmModalState.getOpen(`Вы действительно хотите удалить автомобильный код "${code}"`)
		});
	}

	async function closeRemoveCarCodesConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeCarCodeConfirmModalState.carCodeId == null) throw 'Cannot remove CarCode with ID = null';

			const result = await ProductsProvider.removeProduct(removeCarCodeConfirmModalState.carCodeId);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadCarCodesPage({ ...pagination, page: 1 });
		}

		setRemoveCarCodeConfirmModalState({ carCodeId: null, ...ConfirmModalState.getClosed() });
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
				<Typography variant='h6'>Продукты</Typography>
				<Button variant='add' title='Создать' onClick={() => openCarCodesEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>Код</TableCell>
								<TableCell>Регион</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{
								carCodes.length === 0 &&
								<TableRow>
									<TableCell colSpan={5}>Пусто</TableCell>
								</TableRow>
							}
							{
								carCodes.map(carCode => (
									<TableRow key={`product__${carCode.id}`}>
										<TableCell width='20%'>{carCode.code}</TableCell>
										<TableCell width='40%'>{carCode.region}</TableCell>
										<TableCell>
											<Button
												type='icon'
												variant='edit'
												size='small'
												onClick={() => openCarCodesEditorModal(carCode.id)} />
											<Button
												type='icon'
												variant='remove'
												size='small'
												onClick={() => openRemoveCarCodesConfirmModal(carCode.id, carCode.code)} />
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
					changePage={page => loadCarCodesPage({ ...pagination, page })}
					changeCountInPage={pageSize => loadCarCodesPage({ ...pagination, pageSize })}
				/>
			</Paper>

			<ProductEditorModal
				isOpen={carCodesEditorModalState.isOpen}
				productId={carCodesEditorModalState.carCodeId}
				onClose={closeCarCodesEditorModal}
			/>

			<ConfirmModal
				title={removeCarCodeConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveCarCodesConfirmModal(isConfirmed)}
				isOpen={removeCarCodeConfirmModalState.isOpen}
			/>

			{
				!String.isNullOrWhitespace(errorMessage) &&
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			}
		</Container>
	);
}
