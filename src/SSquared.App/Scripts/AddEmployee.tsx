import React from "react";
import { addEmployee } from "./DataAccess/EmployeeDataAccess";
import { IModifyEmployeeDto } from "./DTO/IModifyEmployeeDto";
import { EmployeeEditor } from "./SharedComponents/EmployeeEditor";
import { IExpandedEmployeeDto } from "./DTO/IExpandedEmployeeDto";
import ReactDOM from "react-dom";

export function init() {
    var element = document.getElementsByClassName("react-wrapper")[0];
    ReactDOM.render(<AddEmployeePage></AddEmployeePage>, element);
}

class AddEmployeePage extends React.Component<AddEmployeePageProps, AddEmployeePageState> {
    constructor(p: AddEmployeePageProps) {
        super(p);

        this.saveEmployee = this.saveEmployee.bind(this);
    }

    public render() {
        var employee: IExpandedEmployeeDto = {
            id:0,
            firstName: "",
            lastName: "",
            employeeId: "",
            getUrl: "",
            updateUrl: "",
            viewOrgChartUrl:"",
            viewUrl:"",
            employees: [],
            manager: null,
            roles:[]
        };

        return <EmployeeEditor employee={employee} key="employeeEditor" onSave={ this.saveEmployee} ></EmployeeEditor>
    }

    async saveEmployee(firstName: string, lastName: string, employeeId: string, managerId: number | null, roleIds: number[]): Promise<void> {
        var dto: IModifyEmployeeDto = {
            firstName: firstName,
            lastName: lastName,
            employeeId: employeeId,
            managerId: managerId,
            roleIds:roleIds
        };

        await addEmployee(dto);

        alert("Employee created!");
        window.location.href = "./";
    }
}

class AddEmployeePageProps {

}

class AddEmployeePageState {

}
