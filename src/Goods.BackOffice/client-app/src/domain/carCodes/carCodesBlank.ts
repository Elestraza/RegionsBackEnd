import { CarCodes } from './carCodes';

export class CarCodesBlank {
	constructor(
		public id: string | null,
		public code: number | null,
		public region: string | null
	) { }
}

export namespace CarCodesBlank {
	export function getEmpty(): CarCodesBlank {
		return new CarCodesBlank(null, null, null);
	}

	export function getFromCarCode(carCode: CarCodes): CarCodesBlank {
		return {
			id: carCode.id,
			code: carCode.code,
			region: carCode.region
		};
	}
}
