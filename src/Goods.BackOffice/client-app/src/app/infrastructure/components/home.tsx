import React from "react";
import { Button } from "../../../shared/components/buttons/button";
import { useNavigate } from "react-router-dom";
import { Links } from "../../router";
import { Box } from "@mui/material";

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

    return (
    <>
        <Box sx={{
            marginLeft: 'auto',
            marginRight: 'auto'
        }}>
            <Box>
                <h1>ПАНЕЛЬ УПРАВЛЕНИЯ РЕГИОНАМИ</h1>
            </Box>
            <Box>
                <Button sx={{marginLeft: 5}} title="Автомобильные коды" variant='edit' onClick={routeCarCodes}/>
                <Button sx={{marginLeft: 5}} title="Регионы" 			variant='edit' onClick={routeRegions}/>
                <Button sx={{marginLeft: 5}} title="Населенные пункты" 	variant='edit' onClick={routeSettlements}/>
            </Box>
        </Box>
    </>
    )
}