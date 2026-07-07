import React from 'react';
import { Route } from 'react-router-dom';
import { ProductsPage } from './products/productsPage';
import { SettlementsPage } from './settlements/settlementsPage';
import { SettlementsTypesPage } from './settlementsTypes/settlementsTypesPage';
import { RegionsPage } from './regions/regionsPage';
import { FederalRegionsPage } from './federalRegions/federalRegionsPage';
import { CarCodesPage } from './carCodes/carCodesPage'


export function Router() {
	return (
		<>
			<Route path={Links.index} element={<ProductsPage />} />
			<Route path={Links.settlements} element={<SettlementsPage />} />
			<Route path={Links.settlementsTypes} element={<SettlementsTypesPage />} />
			<Route path={Links.regions} element={<RegionsPage />} />
			<Route path={Links.federalRegions} element={<FederalRegionsPage />} />
			<Route path={Links.carCodes} element={<CarCodesPage />} />
		</>
	);
}

export class Links {
	public static index = '/products';
	public static settlements = '/settlements';
	public static settlementsTypes = '/settlements-types';
	public static regions = '/regions';
	public static federalRegions = '/federal-regions';
	public static carCodes = '/car-codes';
}
