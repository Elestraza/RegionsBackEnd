import { Page } from '../../tools/types/page';

export class CarCodes {
	constructor(
		public readonly id: string,
		public readonly code: number,
		public readonly region: string
	) { }
}

export function mapToCarCodesPage(data: any): Page<CarCodes> {
	return Page.convert(data, mapToCarCode);
}

export function mapToProducts(data: any[]): CarCodes[] {
	return data.map(mapToCarCode);
}

export function mapToCarCode(data: any): CarCodes {
	return new CarCodes(data.id, data.code, data.region);
}
