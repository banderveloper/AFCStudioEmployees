export default interface IEmployee {
    employeeId: number;
    lastName: string;
    firstName: string;
    middleName: string | null;
    birthDate: string;
    employeeInviteTime: string;
    employeeSalary: number;
    departmentId: number;
}