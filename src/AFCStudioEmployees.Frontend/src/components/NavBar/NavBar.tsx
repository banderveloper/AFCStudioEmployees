import classes from "./NavBar.module.css";
import {Link} from "react-router-dom";

export default function NavBar() {

    return (
        <header>
            <nav className={classes.navbar}>
                <Link to='/' className={classes.navbarLogo}>AFC Studio Employees</Link>
                <ul className={classes.navbarNav}>
                    <Link to='/' className={classes.navItem}>About</Link>
                    <Link to='/employees' className={classes.navItem}>Employees</Link>
                </ul>
            </nav>
        </header>
    )
}