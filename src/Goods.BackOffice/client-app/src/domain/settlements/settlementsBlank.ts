import { Settlements } from './settlements';
import { ProductCategory } from './productCategory';

export class SettlementsBlank {
	constructor(
		public id: string | null,
		public category: ProductCategory | null,
		public name: string | null,
		public description: string | null,
		public price: number | null
	) { }
}

export namespace SettlementsBlank {
	export function getEmpty(): SettlementsBlank {
		return new SettlementsBlank(null, null, null, null, null);
	}

	export function getFromProduct(product: Settlements): SettlementsBlank {
		return {
			id: product.id,
			category: product.category,
			name: product.name,
			description: product.description,
			price: product.price
		};
	}
}
