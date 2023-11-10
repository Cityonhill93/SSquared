import { IEmployeeDto } from "./IEmployeeDto";

export interface IExpandedEmployeeDto extends IEmployeeDto {
    employees: IEmployeeDto[];
    manager: IEmployeeDto | undefined;
}