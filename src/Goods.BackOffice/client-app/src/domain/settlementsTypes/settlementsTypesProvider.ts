import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToSettlementsType, mapToSettlementsTypesPage, SettlementsTypes } from './settlementsTypes';
import { SettlementsTypesBlank } from './settlementsTypesBlank';

export class SettlementsTypesProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveSettlementsType(blank: SettlementsTypesBlank): Promise<Result> {
		const response = await fetch('/settlements-types/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(blank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getSettlementsTypesPage(page: number, count: number): Promise<Page<SettlementsTypes>> {
		const response = await fetch(`/settlements-types/get-page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToSettlementsTypesPage(json);
	}

	public static async getSettlementsTypeById(id: string): Promise<SettlementsTypes | null> {
		const response = await fetch(`/settlements-types/get-by-id?id=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToSettlementsType(json);
	}

	public static async removeSettlementsType(id: string): Promise<Result> {
		const response = await fetch(`/settlements-types/remove?id=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
