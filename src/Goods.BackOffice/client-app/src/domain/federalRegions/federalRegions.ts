import { Page } from '../../tools/types/page';

export class FederalRegions {
	constructor(
		public readonly id: string,
		public readonly name: string
	) { }
}

export function mapToFederalRegionsPage(data: any): Page<FederalRegions> {
	return Page.convert(data, mapToFederalRegion);
}

export function mapToFederalRegions(data: any[]): FederalRegions[] {
	return data.map(mapToFederalRegion);
}

export function mapToFederalRegion(data: any): FederalRegions {
	return new FederalRegions(data.id, data.name);
}
