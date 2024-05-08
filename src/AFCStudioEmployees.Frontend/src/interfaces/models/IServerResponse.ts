export default interface IServerResponsePayload<T> {
    errorCode: string | null;
    data: T,
    succeed: boolean
}