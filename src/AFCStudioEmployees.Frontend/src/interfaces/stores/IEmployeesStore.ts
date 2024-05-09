import IEmployee from "../entities/IEmployee.ts";
import IGetEmployeesRequest from "../models/requests/IGetEmployeesRequest.ts";
import ICreateEmployeeRequest from "../models/requests/ICreateEmployeeRequest.ts";
import IUpdateEmployeeRequest from "../models/requests/IUpdateEmployeeRequest.ts";

export default interface IEmployeesStore {

    isLoading: boolean;
    errorCode: string | null;

    employees: IEmployee[];

    getEmployees: (request: IGetEmployeesRequest) => Promise<void>;
    createEmployee: (request: ICreateEmployeeRequest) => Promise<void>;
    updateEmployee: (request: IUpdateEmployeeRequest) => Promise<void>;
    deleteEmployee: (employeeId: number) => Promise<void>;
}