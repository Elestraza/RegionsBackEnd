import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToRegion, mapToRegionsPage, Regions } from './regions';
import { RegionsBlank } from './regionsBlank';

export class RegionsProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveRegion(blank: RegionsBlank): Promise<Result> {
		const response = await fetch('/regions/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(blank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getRegionsPage(page: number, count: number): Promise<Page<Regions>> {
		const response = await fetch(`/regions/get-page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();
		
		return mapToRegionsPage(json);
	}

	public static async getRegionById(id: string): Promise<Regions | null> {
		const response = await fetch(`/regions/get-by-id?id=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToRegion(json);
	}

	public static async removeRegion(id: string): Promise<Result> {
		const response = await fetch(`/regions/remove?id=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
