import classes from "./EmployeesTable.module.css";
import {useEffect, useState} from "react";
import {usePreviewsStore} from "../../stores/usePreviewsStore.ts";
import {useEmployeesStore} from "../../stores/useEmployeesStore.ts";
import EmployeeTableRow from "../EmployeeTableRow/EmployeeTableRow.tsx";

export default function EmployeesTable() {

    const [searchText, setSearchText] = useState<string>('');
    const [page] = useState<number>(1);
    const [sortBy, setSortBy] = useState<string>('EmployeeId');

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
        doSearch()

    }, []);

    useEffect(() => {
        doSearch()
    }, [sortBy, page])

    return (
        <>
            <div className={classes.searchBlock}>
                <label htmlFor='searchInput' className={classes.searchLabel}>Search: </label>
                <input type="text" className={classes.searchInput} id='searchInput' value={searchText}
                       onChange={e => setSearchText(e.target.value)}/>
                <button className={classes.searchButton} onClick={doSearch}>Go</button>
            </div>

            <table className={classes.employeesTable}>
                <thead>
                <tr>
                    <th onClick={() => setSortBy('EmployeeId')}>Id</th>
                    <th onClick={() => setSortBy('LastName')}>Last name</th>
                    <th onClick={() => setSortBy('FirstName')}>First name</th>
                    <th onClick={() => setSortBy('MiddleName')}>Middle name</th>
                    <th onClick={() => setSortBy('BirthDate')}>Birth date</th>
                    <th onClick={() => setSortBy('EmployeeInviteTime')}>Invite time</th>
                    <th onClick={() => setSortBy('EmployeeSalary')}>Salary</th>
                    <th onClick={() => setSortBy('DepartmentId')}>Department</th>
                </tr>
                </thead>

                <tbody>
                {
                    employeesStore.isLoading || previewsStore.isLoading
                        ?
                        <tr><td>Loading</td></tr>
                        :
                        employeesStore.employees.map(employee => (
                            <EmployeeTableRow
                                key={employee.employeeId}
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