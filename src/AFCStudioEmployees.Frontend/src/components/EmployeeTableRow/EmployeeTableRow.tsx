import IEmployeeTableRow from "../../interfaces/components/IEmployeeTableRow.ts";
import classes from "./EmployeeTableRow.module.css";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import {faTrash} from "@fortawesome/free-solid-svg-icons";

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
            <td>
                <button className={classes.deleteButton}
                        onClick={() => props.deleteEmployee(props.employeeId)}>
                    <FontAwesomeIcon icon={faTrash}/>
                </button>
            </td>
        </tr>
    )
}