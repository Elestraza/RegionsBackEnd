import { Page } from '../../tools/types/page';
import { ProductCategory } from './productCategory';

export class FederalRegions {
	constructor(
		public readonly id: string,
		public readonly category: ProductCategory,
		public readonly name: string,
		public readonly description: string,
		public readonly price: number
	) { }
}

export function mapToFederalRegionsPage(data: any): Page<FederalRegions> {
	return Page.convert(data, mapToFederalRegion);
}

export function mapToFederalRegions(data: any[]): FederalRegions[] {
	return data.map(mapToFederalRegion);
}

export function mapToFederalRegion(data: any): FederalRegions {
	return new FederalRegions(data.id, data.category, data.name, data.description, data.price);
}
