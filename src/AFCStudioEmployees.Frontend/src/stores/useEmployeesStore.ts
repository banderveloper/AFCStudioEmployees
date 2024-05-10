import {create} from "zustand";
import IEmployeesStore from "../interfaces/stores/IEmployeesStore.ts";
import api from "../shared/axios.ts";
import IEmployee from "../interfaces/entities/IEmployee.ts";
import IServerResponsePayload from "../interfaces/models/IServerResponse.ts";
import {ENDPOINTS} from "../shared/endpoints.ts";

export const useEmployeesStore = create<IEmployeesStore>((set) => ({

    isLoading: false,
    errorCode: null,

    employees: [],

    getEmployees: async (request) => {

        set({isLoading: true});

        const response = await api.get<IServerResponsePayload<IEmployee[]>>(ENDPOINTS.EMPLOYEES, {
            params: {
                page: request.page,
                size: request.size,
                search: request.search,
                sortBy: request.sortBy
            }
        });

        if (response.data.succeed) {
            set({employees: response.data.data});
        }

        set({errorCode: response.data.errorCode});
        set({isLoading: false});
    },

    createEmployee: async (request) => {

        set({isLoading: true});

        const response = await api.post<IServerResponsePayload<void>>(ENDPOINTS.EMPLOYEES, request);

        set({errorCode: response.data.errorCode});
        set({isLoading: false});
    },

    updateEmployee: async (request) => {

        set({isLoading: true});

        const response = await api.put<IServerResponsePayload<void>>(ENDPOINTS.EMPLOYEES, request);

        set({errorCode: response.data.errorCode});
        set({isLoading: false});
    },

    deleteEmployee: async (employeeId) => {

        set({isLoading: true});

        const response = await api.delete<IServerResponsePayload<void>>(ENDPOINTS.EMPLOYEES + `/${employeeId}`);

        if (response.data.succeed) {
            set(state => ({
                employees: state.employees.filter(e => e.employeeId != employeeId)
            }));
        }

        set({errorCode: response.data.errorCode});
        set({isLoading: false});
    }
}));