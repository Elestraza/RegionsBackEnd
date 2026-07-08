import React from 'react';
import { Route } from 'react-router-dom';
import { SettlementsPage } from './settlements/settlementsPage';
import { RegionsPage } from './regions/regionsPage';
import { CarCodesPage } from './carCodes/carCodesPage'


export function Router() {
	return (
		<>
			<Route path='settlements' element={<SettlementsPage />} />
			<Route path='regions' element={<RegionsPage />} />
			<Route path='car-codes' element={<CarCodesPage />} />
		</>
	);
}

export class Links {
	public static settlements = "settlements";
	public static regions = "regions";
	public static carCodes = "car-codes";
}