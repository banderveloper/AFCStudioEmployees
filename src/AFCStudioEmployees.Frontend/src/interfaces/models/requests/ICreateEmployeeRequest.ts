export default interface ICreateEmployeeRequest {
    lastName: string;
    firstName: string;
    middleName: string | null;
    birthDate: string;
    jobId: number;
    departmentId: number;
}