import React from "react";
import { IEmployeeDto } from "../DTO/IEmployeeDto";
import { getEmployees } from "../DataAccess/EmployeeDataAccess";
import { extend } from "jquery";

export class EmployeeTable extends React.Component<EmployeeTableProps, EmployeeTableState> {
    constructor(p: EmployeeTableProps) {
        super(p);
    }

    public render() {
        var employees = this.props.data;;
        var rows = employees.map(e => <EmployeeTableRow employee={e} key={`EmployeeRow${e.id}`} ></EmployeeTableRow>);

        return <table style={{ width: "100%" }} >
            <thead>
                <tr>
                    <th>Employee ID</th>
                    <th>First Name</th>
                    <th>Last Name</th>
                </tr>
            </thead>
            <tbody>
                {rows}
            </tbody>
        </table>;
    }

}

export class EmployeeTableProps {
    data: IEmployeeDto[];
}

class EmployeeTableState {

}

class EmployeeTableRow extends React.Component<EmployeeTableRowProps, EmployeeTableRowState> {
    public render() {
        var employee = this.props.employee;

        return <tr>
            <td>{employee.employeeId}</td>
            <td>{employee.firstName}</td>
            <td>{employee.lastName}</td>
        </tr>;
    }
}

class EmployeeTableRowProps {
    employee: IEmployeeDto;
}

class EmployeeTableRowState {

}