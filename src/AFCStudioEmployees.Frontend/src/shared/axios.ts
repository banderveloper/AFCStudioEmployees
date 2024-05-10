import axios, {AxiosInstance} from "axios";
import {API_URL} from "./endpoints.ts";

const api : AxiosInstance = axios.create({
    baseURL: API_URL,
    withCredentials: false
});

api.interceptors.request.use(request => {

    console.log('[INTERCEPTOR] REQUEST');
    console.log(request);

    return request;
});

api.interceptors.response.use(response => {

    console.log('[INTERCEPTOR] RESPONSE');
    console.log(response);

    return response;
},
    async error => {

    console.error('[INTERCEPTOR] RESPONSE ERROR');
    console.error(error);

    return error;
});

export default api;