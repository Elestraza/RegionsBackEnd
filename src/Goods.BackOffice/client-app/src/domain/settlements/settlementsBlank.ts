import { Regions } from '../regions/regions';
import { Settlements } from './settlements';
import { SettlementsTypes } from './settlementsTypes';

export class SettlementsBlank {
	constructor(
		public id: string | null,
		public settlementtype: SettlementsTypes | null,
		public name: string | null,
		public population: number | null,
		public region: Regions | null,
		public foundationdate: number | null,
		public ishero: boolean | null,
		public averagehotelcost: number | null
	) { }
}

export namespace SettlementsBlank {
	export function getEmpty(): SettlementsBlank {
		return new SettlementsBlank(null, null, null, null, null, null, null, null);
	}

	export function getFromSettlement(settlement: Settlements): SettlementsBlank {
		return {
			id: settlement.id,
			settlementtype: settlement.settlementtype,
			name: settlement.name,
			population: settlement.population,
			region: settlement.region,
			foundationdate: settlement.foundationdate,
			ishero: settlement.ishero,
			averagehotelcost: settlement.averagehotelcost
		};
	}
}
