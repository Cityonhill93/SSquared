export interface IModifyEmployeeDto {
    firstName: string;
    lastName: string;
    employeeId: string;
    managerId: number | null;
    roleIds:number[]
}