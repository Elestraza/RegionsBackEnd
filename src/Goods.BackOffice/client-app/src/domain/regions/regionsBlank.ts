import { FederalRegions } from '../federalRegions/federalRegions';
import { Regions } from './regions';
export class RegionsBlank {
	constructor(
		
		public id: string | null,
		public name: string | null,
		public federalRegion: FederalRegions | null
	) { }
}

export namespace RegionsBlank {
	export function getEmpty(): RegionsBlank {
		return new RegionsBlank(null, null, null);
	}
	
	export function getFromRegion(region: Regions): RegionsBlank {
		return {
			id: region.id,
			name: region.name,
			federalRegion: region.federalRegion
		};
	}
}
