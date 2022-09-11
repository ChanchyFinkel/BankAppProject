import { OperationHistory } from "./operationHistory.model";

export interface OperationDataList{
    operations: OperationHistory[],
    totalRows: number
}