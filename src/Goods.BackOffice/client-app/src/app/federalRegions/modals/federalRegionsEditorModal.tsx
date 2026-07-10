import React, { useEffect, useState } from 'react';
import { Button } from '../../../shared/components/buttons/button';
import { Input } from '../../../shared/components/inputs/input';
import { Modal } from '../../../shared/components/modals/modal';
import { Notification } from '../../../shared/components/notification';

import { FederalRegions } from '../../../domain/federalRegions/federalRegions';
import { FederalRegionsBlank } from '../../../domain/federalRegions/federalRegionsBlank';
import { FederalRegionsProvider } from '../../../domain/federalRegions/federalRegionsProvider';

interface Props {
	federalRegionsId: string | null;
	onClose: (isEdited: boolean) => void;
	isOpen: boolean;
}

export function FederalRegionsEditorModal(props: Props) {
	const [blank, setFederalRegionsBlank] = useState<FederalRegionsBlank>(FederalRegionsBlank.getEmpty());
	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		if (!props.isOpen) return;

		async function loadFederalRegionsBlank() {
			let blank: FederalRegionsBlank | null = null;

			if (props.federalRegionsId != null) {
				const federalRegion: FederalRegions | null = await FederalRegionsProvider.getFederalRegionById(props.federalRegionsId);
				if (federalRegion == null) throw 'Car code is null';

				blank = FederalRegionsBlank.getFromFederalRegion(federalRegion);
			}

			setFederalRegionsBlank(blank ?? FederalRegionsBlank.getEmpty());
		}

		loadFederalRegionsBlank();

		return () => {
			setFederalRegionsBlank(FederalRegionsBlank.getEmpty());
			setErrorMessage(null);
		};
	}, [props.isOpen, props.federalRegionsId]);

	async function saveFederalRegion() {
		const result = await FederalRegionsProvider.saveFederalRegion(blank);
		if (!result.isSuccess) {
			setErrorMessage(result.errorsAsString);
			return;
		}

		props.onClose(true);
	}

	return (
		<>
			<Modal onClose={() => props.onClose(false)} isOpen={props.isOpen}>
				<Modal.Header onClose={() => props.onClose(false)}>Редактор федеральных регионов</Modal.Header>
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
						title='Введите название'
						value={blank.name}
						onChange={(name) => setFederalRegionsBlank(prev => ({ ...prev, name }))}
						required
					/>
					<Input
						variant='number'
						title='Введите возраст для определения исторической ценности'
						value={blank.historicalValueAge}
						onChange={(historicalValueAge) => setFederalRegionsBlank(prev => ({ ...prev, historicalValueAge }))}
						required
					/>
				</Modal.Body>
				<Modal.Footer>
					<Button variant='save' onClick={() => {
						saveFederalRegion()
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