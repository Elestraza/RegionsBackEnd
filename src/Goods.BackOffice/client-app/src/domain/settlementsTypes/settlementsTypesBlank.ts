import { SettlementsTypes } from './settlementsTypes';
export class SettlementsTypesBlank {
	constructor(
		public id: string | null,
		public name: string | null
	) { }
}

export namespace SettlementsTypesBlank {
	export function getEmpty(): SettlementsTypesBlank {
		return new SettlementsTypesBlank(null, null);
	}

	export function getFromProduct(product: SettlementsTypes): SettlementsTypesBlank {
		return {
			id: product.id,
			name: product.name
		};
	}
}
