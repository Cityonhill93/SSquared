import React from "react";
import { addEmployee, getEmployee, updateEmployee } from "./DataAccess/EmployeeDataAccess";
import { IModifyEmployeeDto } from "./DTO/IModifyEmployeeDto";
import { EmployeeEditor } from "./SharedComponents/EmployeeEditor";
import { IExpandedEmployeeDto } from "./DTO/IExpandedEmployeeDto";
import ReactDOM from "react-dom";

export function init(id:number) {
    var element = document.getElementsByClassName("react-wrapper")[0];
    ReactDOM.render(<UpdateEmployeePage employeeId={id} ></UpdateEmployeePage>, element);
}

class UpdateEmployeePage extends React.Component<UpdateEmployeePageProps, UpdateEmployeePageState> {
    constructor(p: UpdateEmployeePageProps) {
        super(p);

        this.getState = this.getState.bind(this);
        this.saveEmployee = this.saveEmployee.bind(this);
    }

    public render() {
        var state = this.getState();
        if (!state.employee) {
            return null;
        }

        return <EmployeeEditor employee={state.employee} key="employeeEditor" onSave={this.saveEmployee} ></EmployeeEditor>
    }

    async componentDidMount() {
        var state = this.getState();
        state.employee = await getEmployee(this.props.employeeId);
        this.setState(state);
    }

    getState(): UpdateEmployeePageState {
        var state = this.state;
        if (!state) {
            state = {
                employee: null
            };
        }

        return state;
    }

    async saveEmployee(firstName: string, lastName: string, employeeId: string, managerId: number | null, roleIds: number[]): Promise<void> {
        var dto: IModifyEmployeeDto = {
            firstName: firstName,
            lastName: lastName,
            employeeId: employeeId,
            managerId: managerId,
            roleIds: roleIds
        };

        await updateEmployee(this.props.employeeId, dto);

        alert("Employee Updated!");
        window.location.href = "./";
    }
}

class UpdateEmployeePageProps {
    employeeId: number;
}

class UpdateEmployeePageState {
    employee:IExpandedEmployeeDto|null
}
