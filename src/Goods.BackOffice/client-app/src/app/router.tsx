import React from 'react';
import { Route } from 'react-router-dom';
import { SettlementsPage } from './settlements/settlementsPage';
import { RegionsPage } from './regions/regionsPage';
import { CarCodesPage } from './carCodes/carCodesPage'
import { Home } from './infrastructure/home';
import { FederalRegionsPage } from './federalRegions/federalRegionsPage';

export function Router() {
	return (
		<>
			<Route path={Links.settlements} element={<SettlementsPage />} />
			<Route path={Links.regions} 	element={<RegionsPage />} />
			<Route path={Links.carCodes} 	element={<CarCodesPage />} />
			<Route path={Links.federalRegions} 	element={<FederalRegionsPage />} />
			<Route path={Links.index} 		element={<Home />} />
		</>
	);
}

export class Links {
	public static index = "/"
	public static settlements = "/settlements";
	public static regions = "/regions";
	public static carCodes = "/car-codes";
	public static federalRegions = "/federal-regions";
}