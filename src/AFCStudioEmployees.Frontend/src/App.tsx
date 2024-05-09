import NavBar from "./components/NavBar/NavBar.tsx";
import {Route, Routes} from "react-router-dom";
import AboutPage from "./pages/AboutPage.tsx";
import EmployeesPage from "./pages/EmployeesPage.tsx";

export default function App() {

    return (
        <>
            <NavBar/>
            <Routes>
                <Route path='/' element={<AboutPage/>}/>
                <Route path='/employees' element={<EmployeesPage/>}/>
            </Routes>
        </>
    )
}
