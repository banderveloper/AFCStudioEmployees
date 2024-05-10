import classes from "./EmployeesPage.module.css";
import EmployeesTable from "../../components/EmployeesTable/EmployeesTable.tsx";

export default function EmployeesPage() {

    return (
        <main>
            <h1 className={classes.pageTitle}>Employees</h1>
            <EmployeesTable/>
        </main>
    )
}