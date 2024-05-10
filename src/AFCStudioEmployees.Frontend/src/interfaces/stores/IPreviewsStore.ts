import IDepartmentPreview from "../entities/IDepartmentPreview.ts";
import IJobPreview from "../entities/IJobPreview.ts";

export default interface IPreviewsStore {

    isLoading: boolean;
    departmentsPreviews: IDepartmentPreview[];
    jobsPreviews: IJobPreview[];

    getDepartmentsPreviews: () => Promise<void>;
    getJobsPreviews : () => Promise<void>;
}