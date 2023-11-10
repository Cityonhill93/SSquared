import $ from "jquery";
import { IRoleDto } from "../DTO/IRoleDto";

const baseApiPath = "/api/1/Roles";

export async function getRole(id: number): Promise<IRoleDto> {
    var url = `${baseApiPath}/${id}`;
    return $.get(url);
}

export async function getRoles(): Promise<IRoleDto[]> {
    var url = baseApiPath;

    return $.get(url);
}