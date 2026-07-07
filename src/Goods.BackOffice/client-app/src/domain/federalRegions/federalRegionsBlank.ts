import { FederalRegions } from './FederalRegions';
import { ProductCategory } from './productCategory';

export class FederalRegionsBlank {
	constructor(
		public id: string | null,
		public category: ProductCategory | null,
		public name: string | null,
		public description: string | null,
		public price: number | null
	) { }
}

export namespace FederalRegionsBlank {
	export function getEmpty(): FederalRegionsBlank {
		return new FederalRegionsBlank(null, null, null, null, null);
	}

	export function getFromProduct(product: FederalRegions): FederalRegionsBlank {
		return {
			id: product.id,
			category: product.category,
			name: product.name,
			description: product.description,
			price: product.price
		};
	}
}
