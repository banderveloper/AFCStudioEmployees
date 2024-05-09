import {Link} from "react-router-dom";

export default function NavBar() {

    return (
        <header>
            <nav>
                <h1>Navigation bar</h1>
                <Link to='/'>To about page</Link>
                <Link to='/employees'>To employees page</Link>
            </nav>
        </header>
    )
}