import { Page } from '../../tools/types/page';
import { Regions } from '../regions/regions';
import { SettlementsTypes } from './settlementsTypes';
import { mapToRegion } from '../regions/regions';

export class Settlements {
	constructor(
		public readonly id: string,
		public readonly type: SettlementsTypes,
		public readonly name: string,
		public readonly population: number,
		public readonly region: Regions,
		public readonly foundationYear: number,
		public readonly isHero: boolean,
		public readonly averageHotelCost: number
	) { }
}

export function mapToSettlementsPage(data: any): Page<Settlements> {
	return Page.convert(data, mapToSettlement);
}

export function mapToSettlements(data: any[]): Settlements[] {
	return data.map(mapToSettlement);
}

export function mapToSettlement(data: any): Settlements {
	return new Settlements(data.id, data.type, data.name, data.population, mapToRegion(data.region), data.foundationYear, data.isHero, data.averageHotelCost);
}
