export default interface IUpdateEmployeeRequest {
    employeeId: number;
    lastName: string;
    firstName: string;
    middleName: string | null;
    birthDate: string;
    jobId: number;
    departmentId: number;
}