import { OperationHistory } from "./operation-history.model";

export interface OperationDataList{
    operations: OperationHistory[],
    totalRows: number
}