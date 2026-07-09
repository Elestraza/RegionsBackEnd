import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToCarCode, mapToCarCodesPage, CarCodes } from './carCodes';
import { CarCodesBlank } from './carCodesBlank';

export class CarCodesProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveCarCode(blank: CarCodesBlank): Promise<Result> {
		const response = await fetch('/car-codes/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(blank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getCarCodesPage(page: number, count: number): Promise<Page<CarCodes>> {
		const response = await fetch(`/car-codes/get-page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToCarCodesPage(json);
	}

	public static async getCarCodeById(id: string): Promise<CarCodes | null> {
		const response = await fetch(`/car-codes/get-by-id?id=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToCarCode(json);
	}

	public static async removeCarCodes(id: string): Promise<Result> {
		const response = await fetch(`/car-codes/remove?id=${id}`, {
			method: 'POST',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
