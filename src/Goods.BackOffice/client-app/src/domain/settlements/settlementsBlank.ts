import { Regions } from '../regions/regions';
import { Settlements } from './settlements';
import { SettlementsTypes } from './settlementsTypes';

export class SettlementsBlank {
	constructor(
		public id: string | null,
		public type: SettlementsTypes | null,
		public name: string | null,
		public population: number | null,
		public region: Regions | null,
		public foundationYear: number | null,
		public isHero: boolean | null,
		public averageHotelCost: number | null
	) { }
}

export namespace SettlementsBlank {
	export function getEmpty(): SettlementsBlank {
		return new SettlementsBlank(null, null, null, null, null, null, false, null);
	}

	export function getFromSettlement(settlement: Settlements): SettlementsBlank {
		return {
			id: settlement.id,
			type: settlement.type,
			name: settlement.name,
			population: settlement.population,
			region: settlement.region,
			foundationYear: settlement.foundationYear,
			isHero: settlement.isHero,
			averageHotelCost: settlement.averageHotelCost
		};
	}
}
