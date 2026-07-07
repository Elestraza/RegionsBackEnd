import React from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import { AppBase } from '../shared/components/appBase';
import { Layout } from '../shared/components/layout';
import { InfrastructureRouter } from './infrastructure/infrastructureRouter';
import { Router } from './router'


export function App() {
	return (
		<AppBase>
			<BrowserRouter>
				<Routes>
					<Route element={<Layout />}>
						{InfrastructureRouter()}
						{Router()}
					</Route>
				</Routes>
			</BrowserRouter>
		</AppBase>
	);
}
