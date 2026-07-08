export enum SettlementsTypes {
	City = 1,
	Selo = 2,
	Derevnya = 3,
	PGT = 4
}

export namespace SettlementsTypes {
	export const getDisplayName = (type: SettlementsTypes): string => {
		switch (type) {
			case SettlementsTypes.City:
				return 'Город';
			case SettlementsTypes.Selo:
				return 'Село';
			case SettlementsTypes.Derevnya:
				return 'Деревня';
			case SettlementsTypes.PGT:
				return 'ПГТ';
		}
	};
}
