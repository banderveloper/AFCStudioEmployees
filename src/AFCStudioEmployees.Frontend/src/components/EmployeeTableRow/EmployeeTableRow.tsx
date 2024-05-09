import IEmployeeTableRow from "../../interfaces/components/IEmployeeTableRow.ts";

export default function EmployeeTableRow(props: IEmployeeTableRow) {

    return (
        <tr>
            <td>{props.employeeId}</td>
            <td>{props.lastName}</td>
            <td>{props.firstName}</td>
            <td>{props.middleName}</td>
            <td>{props.birthDate}</td>
            <td>{props.employeeInviteTime}</td>
            <td>{props.employeeSalary}</td>
            <td>{props.departmentName}</td>
        </tr>
    )
}