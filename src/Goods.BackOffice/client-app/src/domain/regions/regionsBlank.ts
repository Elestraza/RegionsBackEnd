import { Regions } from './regions';
import { ProductCategory } from './productCategory';

export class RegionsBlank {
	constructor(
		public id: string | null,
		public category: ProductCategory | null,
		public name: string | null,
		public description: string | null,
		public price: number | null
	) { }
}

export namespace RegionsBlank {
	export function getEmpty(): RegionsBlank {
		return new RegionsBlank(null, null, null, null, null);
	}

	export function getFromProduct(product: Regions): RegionsBlank {
		return {
			id: product.id,
			category: product.category,
			name: product.name,
			description: product.description,
			price: product.price
		};
	}
}
