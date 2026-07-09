import { FederalRegions } from './federalRegions';

export class FederalRegionsBlank {
	constructor(
		
		public id: string | null,
		public name: string | null,
		public historicalValueAge: number | null
	) { }
}

export namespace FederalRegionsBlank {
	export function getEmpty(): FederalRegionsBlank {
		return new FederalRegionsBlank(null, null, null);
	}
	
	export function getFromFederalRegion(region: FederalRegions): FederalRegionsBlank {
		return {
			id: region.id,
			name: region.name,
			historicalValueAge: region.historicalValueAge
		};
	}
}
