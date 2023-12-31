﻿import React, { ChangeEvent } from "react";
import { IEmployeeDto } from "../DTO/IEmployeeDto";
import { IExpandedEmployeeDto } from "../DTO/IExpandedEmployeeDto";
import { EmployeePicker } from "./EmployeePicker";
import { RolePicker } from "./RolePicker";
import { getEmployees, getPotentialManagers } from "../DataAccess/EmployeeDataAccess";
import { isNullOrWhiteSpace } from "../DataTools/StringHelpers";

export class EmployeeEditor extends React.Component<EmployeeEditorProps, EmployeeEditorState> {
    constructor(p: EmployeeEditorProps) {
        super(p);

        this.buttonClicked = this.buttonClicked.bind(this);
        this.employeeIdChanged = this.employeeIdChanged.bind(this);
        this.firstNameChanged = this.firstNameChanged.bind(this);
        this.getManagers = this.getManagers.bind(this);
        this.getState = this.getState.bind(this);
        this.lastNameChanged = this.lastNameChanged.bind(this);
        this.managerChanged = this.managerChanged.bind(this);
        this.rolesChanged = this.rolesChanged.bind(this);
        this.isValid = this.isValid.bind(this);
    }

    public render() {
        const employeeIdInputId = "employeeId";
        const fistNameInputId = "firstName";
        const lastNameInputId = "lastName";
        const managerInputId = "managerId";

        var state = this.getState();
        var saveButton = (this.isValid()
            ? <button className="btn btn-primary mt-3" type="button" onClick={this.buttonClicked}>Save</button> 
            : <button className="btn btn-primary mt-3" type="button" disabled>Save</button>);

        return <div key={`employeeEditor${this.props.employee.id}`}>
            <div className="form-group">
                <label htmlFor={employeeIdInputId}>Employee ID</label>
                <input className="form-control" id={employeeIdInputId} type="text" maxLength={10} onChange={this.employeeIdChanged} value={state.employeeId} ></input>
            </div>
            <div className="form-group">
                <label htmlFor={fistNameInputId}>First Name</label>
                <input className="form-control" id={ fistNameInputId} type="text" maxLength={100} onChange={this.firstNameChanged} value={state.firstName} ></input>
            </div>
            <div className="form-group">
                <label htmlFor={lastNameInputId}>Last Name</label>
                <input className="form-control" id={ lastNameInputId} type="text" maxLength={100} onChange={this.lastNameChanged} value={state.lastName} ></input>
            </div>
            <div className="form-group">
                <label htmlFor={managerInputId}>Manager</label>
                <EmployeePicker key={employeeIdInputId} id={employeeIdInputId} employeeSelected={this.managerChanged} getEmployees={this.getManagers} value={state.managerId} ></EmployeePicker>
            </div>
            <div className="form-group">
                <label>Roles</label>
                <RolePicker key="roles" itemsChanged={this.rolesChanged} selectedRoles={state.roleIds} ></RolePicker>
            </div>
            <div>
                {saveButton}
            </div>
        </div>;
    }

    async buttonClicked() {
        var state = this.getState();

        await this.props.onSave(
            state.firstName,
            state.lastName,
            state.employeeId,
            state.managerId,
            state.roleIds);
    }

    employeeIdChanged(e: ChangeEvent) {
        var value = $(e.target).val()?.toString();

        var state = this.getState();
        state.employeeId = value;
        this.setState(state);
    }

    firstNameChanged(e: ChangeEvent) {
        var value = $(e.target).val()?.toString();

        var state = this.getState();
        state.firstName = value;
        this.setState(state);
    }

    getManagers():Promise<IEmployeeDto[]> {
        var employee = this.props.employee;
        if (employee.id == 0) {
            return getEmployees();
        }
        else {
            return getPotentialManagers(employee.id);
        }
    }

    getState(): EmployeeEditorState {
        var state = this.state;
        if (!state) {
            var employee = this.props.employee;

            state = {
                employeeId: employee.employeeId,
                firstName: employee.firstName,
                lastName: employee.lastName,
                managerId: employee.manager?.id,
                roleIds: employee.roles.map(role => role.id)
            };
        }

        return state;
    }

    lastNameChanged(e: ChangeEvent) {
        var value = $(e.target).val()?.toString();

        var state = this.getState();
        state.lastName = value;
        this.setState(state);
    }

    managerChanged(managerId:number|undefined) {
        var state = this.getState();
        state.managerId = managerId;
        this.setState(state);
    }

    rolesChanged(selectedRoleIds: number[]) {
        var state = this.getState();
        state.roleIds = selectedRoleIds;
        this.setState(state);
    }

    isValid() {
        var state = this.getState();

        return !isNullOrWhiteSpace(state.firstName)
            && !isNullOrWhiteSpace(state.lastName)
            && !isNullOrWhiteSpace(state.employeeId);
    }
}

export class EmployeeEditorProps {
    employee: IExpandedEmployeeDto;
    onSave: (firstName: string, lastName: string, employeeId: string, managerId: number | null, roleIds: number[])=>Promise<void>;
}

class EmployeeEditorState {
    employeeId: string;
    firstName: string;
    lastName: string;
    managerId: number | null;
    roleIds: number[];
}