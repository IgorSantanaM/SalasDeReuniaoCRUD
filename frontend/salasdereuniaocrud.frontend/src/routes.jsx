import React from "react";
import {Routes, Route} from 'react-router-dom';

import Reservas from "./pages/Reservas";
import NotFoundPage from "./pages/NotFound";

export default function RoutesConfig(){
    return(
        <Routes>
            <Route path="/" exact element={<Reservas />}></Route>
            <Route path="*" exact element={<NotFoundPage />}></Route>
        </Routes>
    )
}