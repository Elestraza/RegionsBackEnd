import { FederalRegions } from './federalRegions';

export class FederalRegionsBlank {
	constructor(
		public id: string | null,
		public name: string | null
	) { }
}

export namespace FederalRegionsBlank {
	export function getEmpty(): FederalRegionsBlank {
		return new FederalRegionsBlank(null, null);
	}

	export function getFromFederalRegion(federalRegion: FederalRegions): FederalRegionsBlank {
		return {
			id: federalRegion.id,
			name: federalRegion.name
		};
	}
}
