import { IEmployeeDto } from "./IEmployeeDto";
import { IRoleDto } from "./IRoleDto";

export interface IExpandedEmployeeDto extends IEmployeeDto {
    employees: IEmployeeDto[];
    manager: IEmployeeDto | undefined;
    roles: IRoleDto[];
}