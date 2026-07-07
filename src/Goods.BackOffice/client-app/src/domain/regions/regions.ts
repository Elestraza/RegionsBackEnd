import { Page } from '../../tools/types/page';
import { ProductCategory } from './productCategory';

export class Regions {
	constructor(
		public readonly id: string,
		public readonly category: ProductCategory,
		public readonly name: string,
		public readonly description: string,
		public readonly price: number
	) { }
}

export function mapToRegionsPage(data: any): Page<Regions> {
	return Page.convert(data, mapToRegion);
}

export function mapToProducts(data: any[]): Regions[] {
	return data.map(mapToRegion);
}

export function mapToRegion(data: any): Regions {
	return new Regions(data.id, data.category, data.name, data.description, data.price);
}
