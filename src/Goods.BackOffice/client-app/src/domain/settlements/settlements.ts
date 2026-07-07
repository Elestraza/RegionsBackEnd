import { Page } from '../../tools/types/page';

/*
	id uuid primary key not null,
	settlementtype uuid NOT NULL,
	name varchar NOT NULL,
	population int NOT NULL,
	region uuid NOT NULL,
	foundationyear varchar(4) NOT NULL,
	ishero bool NOT NULL,
	averagehotelcost int NOT NULL,
*/

export class Settlements {
	constructor(
		public readonly id: string,
		public readonly settlementtype: string,
		public readonly name: string,
		public readonly population: number,
		public readonly region: string,
		public readonly foundationdate: string,
		public readonly ishero: boolean,
		public readonly averagehotelcost: number
	) { }
}

export function mapToSettlementsPage(data: any): Page<Settlements> {
	return Page.convert(data, mapToSettlement);
}

export function mapToSettlements(data: any[]): Settlements[] {
	return data.map(mapToSettlement);
}

export function mapToSettlement(data: any): Settlements {
	return new Settlements(data.id, data.settlementtype, data.name, data.population, data.region, data.foundationdate, data.ishero, data.averagehotelcost);
}
