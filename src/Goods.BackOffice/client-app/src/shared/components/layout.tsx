import { AppBar, Box, Typography } from '@mui/material';
import React from 'react';
import { Outlet, useNavigate } from 'react-router-dom';
import { Button } from './buttons/button';
import { Links } from '../../app/router';

export function Layout() {

	let navigate = useNavigate(); 
	const routeCarCodes = () =>{ 
		navigate(Links.carCodes);
	}
	const routeRegions = () =>{ 
		navigate(Links.regions);
	}
	const routeSettlements = () =>{ 
		navigate(Links.settlements);
	}
	const routeHome = () =>{ 
		navigate(Links.index);
	}

	return (
		<>
			<AppBar position='fixed' sx={{ height: 64 }}>
				<Box
					sx={{
						display: 'flex',
						width: '100%',
						height: '100%',
						alignItems: 'center',
						gap: 4,
						paddingX: 2
					}}>
					<Box sx={{ width: 'fit-content', height: '100%', display: 'flex', alignItems: 'center' }}>
						<Typography sx={{ fontWeight: 'bold' }}>Goods</Typography>	
						<Button 	sx={{ color: 'white', marginLeft: 5 }} title="Главный экран" variant='confirm' onClick={routeHome}/>
						<Button sx={{marginLeft: 5}} title="Автомобильные коды" variant='edit' 		onClick={routeCarCodes}/>
						<Button sx={{marginLeft: 5}} title="Регионы" 			variant='edit' 		onClick={routeRegions}/>
						<Button sx={{marginLeft: 5}} title="Населенные пункты" 	variant='edit' 		onClick={routeSettlements}/>
					</Box>
					
				</Box>
				
			</AppBar>
			
			<Box
				sx={{
					width: '100%',
					height: '100%',
					paddingTop: 10,
					paddingBottom: 2,
					paddingX: 2,
				}}>
				<Box>
					<Outlet />
				</Box>
			</Box>
		</>
	);
}
