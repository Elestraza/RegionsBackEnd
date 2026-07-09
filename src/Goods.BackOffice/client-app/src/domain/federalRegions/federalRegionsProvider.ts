import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToRegion, mapToRegionsPage, FederalRegions } from './federalRegions';
import { FederalRegionsBlank } from './federalRegionsBlank';

export class FederalRegionsProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveFederalRegion(blank: FederalRegionsBlank): Promise<Result> {
		const response = await fetch('/federal-regions/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(blank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getFederalRegionsPage(page: number, count: number): Promise<Page<FederalRegions>> {
		const response = await fetch(`/federal-regions/get-page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();
		
		return mapToRegionsPage(json);
	}

	public static async getFederalRegionById(id: string): Promise<FederalRegions | null> {
		const response = await fetch(`/federal-regions/get-by-id?id=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToRegion(json);
	}

	public static async removeFederalRegion(id: string): Promise<Result> {
		const response = await fetch(`/federal-regions/remove?id=${id}`, {
			method: 'POST',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
