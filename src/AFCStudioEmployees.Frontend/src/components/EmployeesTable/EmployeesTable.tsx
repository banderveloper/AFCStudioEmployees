import classes from "./EmployeesTable.module.css";
import {useEffect, useState} from "react";
import {usePreviewsStore} from "../../stores/usePreviewsStore.ts";
import {useEmployeesStore} from "../../stores/useEmployeesStore.ts";
import EmployeeTableRow from "../EmployeeTableRow/EmployeeTableRow.tsx";

export default function EmployeesTable() {

    const [searchText, setSearchText] = useState<string>();
    const [page] = useState<number>(1);
    const [sortBy] = useState<string>('EmployeeId');

    const previewsStore = usePreviewsStore();
    const employeesStore = useEmployeesStore();

    function getDepartmentNameById(departmentId: number): string {
        return previewsStore.departmentsPreviews.filter(department => department.departmentId == departmentId)[0].departmentName;
    }

    function doSearch(): void {
        employeesStore.getEmployees({page: page, search: searchText ? searchText : '', sortBy: sortBy, size: 10});
    }

    useEffect(() => {

        previewsStore.getDepartmentsPreviews();
        employeesStore.getEmployees({page: page, search: '', sortBy: sortBy, size: 10});

    }, []);


    return (
        <>
            <div className={classes.searchBlock}>
                <label htmlFor='searchInput'>Search: </label>
                <input type="text" className={classes.searchInput} id='searchInput' value={searchText}
                       onChange={e => setSearchText(e.target.value)}/>
                <button onClick={doSearch}>Go</button>
            </div>

            <table className={classes.employeesTable}>
                <thead>
                <tr>
                    <th>Id</th>
                    <th>Last name</th>
                    <th>First name</th>
                    <th>Middle name</th>
                    <th>Birth date</th>
                    <th>Invite time</th>
                    <th>Salary</th>
                    <th>Department</th>
                </tr>
                </thead>

                <tbody>
                {
                    employeesStore.isLoading || previewsStore.isLoading
                        ?
                        <h1>Loading</h1>
                        :
                        employeesStore.employees.map(employee => (
                            <EmployeeTableRow
                                employeeId={employee.employeeId}
                                lastName={employee.lastName}
                                firstName={employee.firstName}
                                middleName={employee.middleName}
                                birthDate={employee.birthDate}
                                employeeInviteTime={employee.employeeInviteTime}
                                employeeSalary={employee.employeeSalary}
                                departmentName={getDepartmentNameById(employee.departmentId)}/>
                        ))
                }
                </tbody>

            </table>
        </>
    )

}