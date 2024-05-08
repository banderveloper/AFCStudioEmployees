import {create} from "zustand";
import IPreviewsStore from "../interfaces/stores/IPreviewsStore.ts";
import api from "../shared/axios.ts";
import IServerResponsePayload from "../interfaces/models/IServerResponse.ts";
import IDepartmentPreview from "../interfaces/entities/IDepartmentPreview.ts";
import {ENDPOINTS} from "../shared/endpoints.ts";
import IJobPreview from "../interfaces/entities/IJobPreview.ts";

export const usePreviewsStore = create<IPreviewsStore>((set) => ({

    isLoading: false,
    departmentsPreviews: [],
    jobsPreviews: [],

    getDepartmentsPreviews: async () => {

        set({isLoading: true});

        const response = await api.get<IServerResponsePayload<IDepartmentPreview[]>>(ENDPOINTS.PREVIEWS.DEPARTMENTS);

        if (response.data.succeed){
            set({departmentsPreviews: response.data.data});
        }

        set({isLoading: false});
    },

    getJobsPreviews: async () => {

        set({isLoading: true});

        const response = await api.get<IServerResponsePayload<IJobPreview[]>>(ENDPOINTS.PREVIEWS.JOBS);

        if(response.data.succeed)
            set({jobsPreviews: response.data.data});

        set({isLoading: false});
    }
}));