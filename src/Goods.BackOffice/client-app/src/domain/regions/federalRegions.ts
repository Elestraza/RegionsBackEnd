export enum FederalRegions {
	Moscow = 1,
	Tula = 2,
	KrasnodarskiyKrai = 3,
	YamaloNenetskiyAvtonomniyOkrug = 4,
	Kaluga = 5
}

export namespace FederalRegions {
	export const getDisplayName = (fr: FederalRegions): string => {
		switch (fr) {
			case FederalRegions.Moscow:
				return 'Московская область';
			case FederalRegions.Tula:
				return 'Тульская область';
			case FederalRegions.KrasnodarskiyKrai:
				return 'Краснодарский край';
			case FederalRegions.YamaloNenetskiyAvtonomniyOkrug:
				return 'Ямало-Ненецкий автономный округ';
			case FederalRegions.Kaluga:
				return 'Калужская область';
		}
	};
}
