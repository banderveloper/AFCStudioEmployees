export const API_URL: string = 'http://localhost:5035';

export const ENDPOINTS = {
    ALL_EMPLOYEES: `${API_URL}/employees/all`,
    EMPLOYEES: `${API_URL}/employees`,
    PREVIEWS: {
        DEPARTMENTS: `${API_URL}/previews/departments`,
        JOBS: `${API_URL}/previews/jobs`,
    }
};