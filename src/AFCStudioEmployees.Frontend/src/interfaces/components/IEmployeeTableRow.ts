export default interface IEmployeeTableRow{
    employeeId: number;
    lastName: string;
    firstName: string;
    middleName: string | null;
    birthDate: string;
    employeeInviteTime: string;
    employeeSalary: number;
    departmentName: string;

    deleteEmployee: (employeeId: number) => void;
}