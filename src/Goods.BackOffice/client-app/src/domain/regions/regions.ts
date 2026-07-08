import { Page } from '../../tools/types/page';
import { FederalRegions } from './federalRegions';

export class Regions {
	constructor(
		public readonly id: string,
		public readonly name: string,
		public readonly federalregion: FederalRegions
	) { }
}

export function mapToRegionsPage(data: any): Page<Regions> {
	return Page.convert(data, mapToRegion);
}

export function mapToProducts(data: any[]): Regions[] {
	return data.map(mapToRegion);
}

export function mapToRegion(data: any): Regions {
	return new Regions(data.id, data.name, data.federalregion);
}
