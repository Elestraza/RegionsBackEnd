import React from 'react';
import { Route } from 'react-router-dom';
import { SettlementsPage } from './settlements/settlementsPage';
import { SettlementsTypesPage } from './settlementsTypes/settlementsTypesPage';
import { RegionsPage } from './regions/regionsPage';
import { FederalRegionsPage } from './federalRegions/federalRegionsPage';
import { CarCodesPage } from './carCodes/carCodesPage'


export function Router() {
	return (
		<>
			<Route path='settlements' element={<SettlementsPage />} />
			<Route path='settlements-types' element={<SettlementsTypesPage />} />
			<Route path='regions' element={<RegionsPage />} />
			<Route path='federal-regions' element={<FederalRegionsPage />} />
			<Route path='car-codes' element={<CarCodesPage />} />
		</>
	);
}

export class Links {
	public static settlements = "settlements";
	public static settlementsTypes = "settlements-types";
	public static regions = "regions";
	public static federalRegions = "federal-regions";
	public static carCodes = "car-codes";
}