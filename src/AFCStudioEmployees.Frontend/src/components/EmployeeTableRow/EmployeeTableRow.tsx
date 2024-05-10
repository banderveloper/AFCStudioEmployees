import IEmployeeTableRow from "../../interfaces/components/IEmployeeTableRow.ts";

export default function EmployeeTableRow(props: IEmployeeTableRow) {

    return (
        <tr>
            <td>{props.employeeId}</td>
            <td>{props.lastName}</td>
            <td>{props.firstName}</td>
            <td>{props.middleName}</td>
            <td>{new Date(props.birthDate).toLocaleDateString()}</td>
            <td>{new Date(props.employeeInviteTime).toLocaleDateString()}</td>
            <td>{props.employeeSalary}</td>
            <td>{props.departmentName}</td>
        </tr>
    )
}