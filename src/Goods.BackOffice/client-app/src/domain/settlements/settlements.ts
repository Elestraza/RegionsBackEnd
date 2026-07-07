import { Page } from '../../tools/types/page';
import { ProductCategory } from './productCategory';

export class Settlements {
	constructor(
		public readonly id: string,
		public readonly category: ProductCategory,
		public readonly name: string,
		public readonly description: string,
		public readonly price: number
	) { }
}

export function mapToSettlementsPage(data: any): Page<Settlements> {
	return Page.convert(data, mapToSettlement);
}

export function mapToSettlements(data: any[]): Settlements[] {
	return data.map(mapToSettlement);
}

export function mapToSettlement(data: any): Settlements {
	return new Settlements(data.id, data.category, data.name, data.description, data.price);
}
