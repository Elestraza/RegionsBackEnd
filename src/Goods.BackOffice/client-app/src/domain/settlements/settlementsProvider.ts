import { Page } from '../../tools/types/page';
import { mapToResult, Result } from '../../tools/types/results/result';
import { mapToSettlement, mapToSettlementsPage, Settlements } from './settlements';
import { SettlementsBlank } from './settlementsBlank';

export class SettlementsProvider {
	private static readonly headers: HeadersInit = [
		['X-Requested-With', 'XMLHttpRequest'],
		['Content-Type', 'application/json']
	];

	public static async saveSettlements(productBlank: SettlementsBlank): Promise<Result> {
		const response = await fetch('/settlements/save', {
			method: 'POST',
			headers: this.headers,
			body: JSON.stringify(productBlank)
		});
		const json = await response.json();

		return mapToResult(json);
	}

	public static async getSettlementsPage(page: number, count: number): Promise<Page<Settlements>> {
		const response = await fetch(`/settlements/get-page?page=${page}&count=${count}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();
		
		return mapToSettlementsPage(json);
	}

	public static async getSettlementById(id: string): Promise<Settlements | null> {
		const response = await fetch(`/settlements/get-by-id?id=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToSettlement(json);
	}

	public static async removeSettlement(id: string): Promise<Result> {
		const response = await fetch(`/settlements/remove?id=${id}`, {
			method: 'GET',
			headers: this.headers
		});
		const json = await response.json();

		return mapToResult(json);
	}
}
