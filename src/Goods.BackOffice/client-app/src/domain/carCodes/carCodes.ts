import { Page } from '../../tools/types/page';
import { Regions } from '../regions/regions';
import { mapToRegion } from '../regions/regions';
export class CarCodes {
	constructor(
		public readonly id: string,
		public readonly code: string,
		public readonly regions: Regions
	) { }
}

export function mapToCarCodesPage(data: any): Page<CarCodes> {
	return Page.convert(data, mapToCarCode);
}

export function mapToProducts(data: any[]): CarCodes[] {
	return data.map(mapToCarCode);
}

export function mapToCarCode(data: any): CarCodes {
	return new CarCodes(data.id, data.code, mapToRegion(data.regions));
}
