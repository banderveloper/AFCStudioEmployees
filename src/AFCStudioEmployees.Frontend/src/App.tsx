import {usePreviewsStore} from "./stores/usePreviewsStore.ts";
import {useEffect} from "react";

export default function App() {

    const {departmentsPreviews, getDepartmentsPreviews} = usePreviewsStore();

    useEffect(() => {
        getDepartmentsPreviews()
    }, []);

    useEffect(() => {

        console.log('DEPARTMENTS PREVIEWS CHANGED');
        console.log(departmentsPreviews)

    }, [departmentsPreviews])

    return (
        <div>
            <h1>Hello world</h1>
        </div>
    )
}
