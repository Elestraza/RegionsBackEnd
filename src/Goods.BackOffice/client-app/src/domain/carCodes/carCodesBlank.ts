import { CarCodes } from './CarCodes';
import { ProductCategory } from './productCategory';

export class CarCodesBlank {
	constructor(
		public id: string | null,
		public category: ProductCategory | null,
		public name: string | null,
		public description: string | null,
		public price: number | null
	) { }
}

export namespace CarCodesBlank {
	export function getEmpty(): CarCodesBlank {
		return new CarCodesBlank(null, null, null, null, null);
	}

	export function getFromProduct(carCode: CarCodes): CarCodesBlank {
		return {
			id: carCode.id,
			category: carCode.category,
			name: carCode.name,
			description: carCode.description,
			price: carCode.price
		};
	}
}
