import { Page } from '../../tools/types/page';

export class FederalRegions {
	constructor(
		public readonly id: string,
		public readonly name: string,
		public readonly historicalValueAge: number
	) { }
}

export function mapToRegionsPage(data: any): Page<FederalRegions> {
	return Page.convert(data, mapToRegion);
}

export function mapToRegions(data: any[]): FederalRegions[] {
	return data.map(mapToRegion);
}

export function mapToRegion(data: any): FederalRegions {
	return new FederalRegions(data.id, data.name, data.federalRegion);
}
