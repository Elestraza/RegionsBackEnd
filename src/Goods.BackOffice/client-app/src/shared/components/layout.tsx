import { AppBar, Box, Typography } from '@mui/material';
import React from 'react';
import { Link, Outlet, useNavigate } from 'react-router-dom';
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
	
	const HomeBtnSx = {
		padding: '5px', 
		margin: '5px', 
		fontWeight: 'bold', 
		color: 'white', 
		textDecoration: 'none' 
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
					<Box sx={{ width: 'fit-content', height: '100%', alignItems: 'center', display: 'flex' }}>
						<Typography sx={{ fontWeight: 'bold' }}>Управление населенными пунктами</Typography>	
						{/* <Button 	sx={{ color: 'white', marginLeft: 5 }} title="Главный экран" variant='add' onClick={routeHome}/> */}
						<Typography component={Link} to={Links.index} sx={{ 
							padding: '5px', 
							margin: '5px', 
							fontWeight: 'bold', 
							color: 'white', 
							textDecoration: 'none'  
						}}> 
							ГЛАВНЫЙ ЭКРАН
						</Typography>	
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
