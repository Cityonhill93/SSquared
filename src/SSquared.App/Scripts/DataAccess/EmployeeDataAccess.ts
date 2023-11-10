import { IEmployeeDto } from "../DTO/IEmployeeDto";
import { IExpandedEmployeeDto } from "../DTO/IExpandedEmployeeDto";
import $ from "jquery";

const baseApiPath = "/api/1/Employees";

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