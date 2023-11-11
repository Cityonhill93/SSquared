import { IModifyEmployeeDto } from "../DTO/IModifyEmployeeDto";
import { IEmployeeDto } from "../DTO/IEmployeeDto";
import { IExpandedEmployeeDto } from "../DTO/IExpandedEmployeeDto";
import $, { data } from "jquery";

const baseApiPath = "/api/v1/Employees";

export async function addEmployee(dto: IModifyEmployeeDto):Promise<IExpandedEmployeeDto> {
    var url = baseApiPath;
    var json = JSON.stringify(dto);

    return await $.ajax(url, {
        method: "POST",
        contentType: "application/json",
        data:json
    });
}

export async function getEmployee(id:number): Promise<IExpandedEmployeeDto> {
    var url = `${baseApiPath}/${id}`;
    return $.get(url);
}

export async function getEmployees(filter: string | undefined = null): Promise<IEmployeeDto[]> {
    var url = baseApiPath;
    if (filter && filter.length > 0) {
        url += `?filter=${filter}`;
    }

    return $.get(url);
}

export async function updateEmployee(id: number, dto: IModifyEmployeeDto) {
    var url = `${baseApiPath}/${id}`;
    var json = JSON.stringify(dto);

    return await $.ajax(url, {
        method: "PUT",
        contentType: "application/json",
        data: json
    });
}