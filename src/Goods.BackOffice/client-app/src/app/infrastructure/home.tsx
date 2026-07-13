import React from "react";
import { Button } from "../../shared/components/buttons/button";
import { useNavigate } from "react-router-dom";
import { Links } from "../router";
import { Box, Container, Paper } from "@mui/material";

export function Home() {
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
    const routeFederalRegions = () =>{ 
        navigate(Links.federalRegions);
    }
    return ( <>
        <Container
            display='flex'
            flexDirection='column'
            disableGutters
            gap='10px'
        >
            <Paper 
                display='flex'
                justifyContent="center"
                alignItems="center"
                elevation={3}
                sx={{
                    padding: '10px',
                    gap: '15px'}}
            >
                <Box
                    display="flex"
                    justifyContent="center"
                    alignItems="center"
                    sx={{
                        margin: 'auto'}}
                >
                    <h1>ПАНЕЛЬ УПРАВЛЕНИЯ РЕГИОНАМИ</h1>
                </Box>
                    <Box
                    display="flex"
                    justifyContent="center"
                    alignItems="center"
                    sx={{
                        margin: 'auto',
                        gap: '10px'
                }}>
                    <Button sx={{marginLeft: 5}} title="Автомобильные коды" variant='edit' onClick={routeCarCodes}/>
                    <Button sx={{marginLeft: 5}} title="Регионы" 			variant='edit' onClick={routeRegions}/>
                    <Button sx={{marginLeft: 5}} title="Населенные пункты" 	variant='edit' onClick={routeSettlements}/>
                    <Button sx={{marginLeft: 5}} title="Федеральные регионы" 	variant='edit' onClick={routeFederalRegions}/>
                </Box>
            </Paper>
        </Container>
    </>
    )
}