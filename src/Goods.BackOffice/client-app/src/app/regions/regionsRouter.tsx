import React from 'react';
import { Route } from 'react-router-dom';
import { RegionsPage } from './regionsPage';

export function RegionsRouter() {
	return (
		<>
			<Route path={RegionsLink.index} element={<RegionsPage />} />
		</>
	);
}

export class RegionsLink {
	public static index = '/regions';
}
