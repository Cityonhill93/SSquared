export interface IAddEmployeeDto {
    firstName: string;
    lastName: string;
    employeeId: string;
    managerId: number | null;
}