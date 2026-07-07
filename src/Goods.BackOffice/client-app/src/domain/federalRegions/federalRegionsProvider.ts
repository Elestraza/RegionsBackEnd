import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToFederalRegion, mapToFederalRegionsPage, FederalRegions } from './FederalRegions';
import { FederalRegionsBlank } from './federalRegionsBlank';

export class FederalRegionsProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveFederalRegion(blank: FederalRegionsBlank): Promise<Result> {
		const response = await fetch('/products/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(blank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getFederalRegionsPage(page: number, count: number): Promise<Page<FederalRegions>> {
		const response = await fetch(`/products/get_page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToFederalRegionsPage(json);
	}

	public static async getProductById(id: string): Promise<FederalRegions | null> {
		const response = await fetch(`/products/get_by_id?productId=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToFederalRegion(json);
	}

	public static async removeFederalRegion(id: string): Promise<Result> {
		const response = await fetch(`/products/mark_product_as_removed?productId=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
