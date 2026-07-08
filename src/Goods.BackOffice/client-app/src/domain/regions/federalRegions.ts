export enum FederalRegions {
	Moscow = 1,
	Tula = 2,
	YamaloNenetskiyAvtonomniyOkrug = 3,
	KrasnodarskiyKrai = 4
}

export namespace FederalRegions {
	export const getDisplayName = (fr: FederalRegions): string => {
		switch (fr) {
			case FederalRegions.Moscow:
				return 'Московская область';
			case FederalRegions.Tula:
				return 'Тульская область';
			case FederalRegions.YamaloNenetskiyAvtonomniyOkrug:
				return 'Ямало-Ненецкий автономный округ';
			case FederalRegions.KrasnodarskiyKrai:
				return 'Краснодарский край';
		}
	};
}
