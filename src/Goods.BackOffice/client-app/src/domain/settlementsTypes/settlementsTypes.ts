import { Page } from '../../tools/types/page';

export class SettlementsTypes {
	constructor(
		public readonly id: string,
		public readonly name: string
	) { }
}

export function mapToSettlementsTypesPage(data: any): Page<SettlementsTypes> {
	return Page.convert(data, mapToSettlementsType);
}

export function mapToSettlementsTypes(data: any[]): SettlementsTypes[] {
	return data.map(mapToSettlementsType);
}

export function mapToSettlementsType(data: any): SettlementsTypes {
	return new SettlementsTypes(data.id, data.name);
}
