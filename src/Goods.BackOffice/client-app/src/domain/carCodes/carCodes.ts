import { Page } from '../../tools/types/page';
import { ProductCategory } from './productCategory';

export class CarCodes {
	constructor(
		public readonly id: string,
		public readonly category: ProductCategory,
		public readonly name: string,
		public readonly description: string,
		public readonly price: number
	) { }
}

export function mapToCarCodesPage(data: any): Page<CarCodes> {
	return Page.convert(data, mapToCarCode);
}

export function mapToProducts(data: any[]): CarCodes[] {
	return data.map(mapToCarCode);
}

export function mapToCarCode(data: any): CarCodes {
	return new CarCodes(data.id, data.category, data.name, data.description, data.price);
}
