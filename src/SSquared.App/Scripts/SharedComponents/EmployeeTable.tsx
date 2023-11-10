import React from "react";
import { IEmployeeDto } from "../DTO/IEmployeeDto";
import { getEmployees } from "../DataAccess/EmployeeDataAccess";
import { extend } from "jquery";

export class EmployeeTable extends React.Component<EmployeeTableProps, EmployeeTableState> {
    constructor(p: EmployeeTableProps) {
        super(p);

        this.getState = this.getState.bind(this);
    }

    public render() {
        var state = this.getState();
        if (!state.loaded) {
            return null;
        }

        var employees = state.items;
        var rows = employees.map(e => <EmployeeTableRow employee={e} key={`EmployeeRow${e.id}`} ></EmployeeTableRow>);

        return <table>
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

    async componentDidMount() {
        var state = this.getState();
        if (!state.loaded) {
            var items = await getEmployees(this.props.filter);
            state.loaded = true;
            state.items = items;
            this.setState(state);
        }
    }

    getState(): EmployeeTableState {
        var state = this.state;
        if (!state) {
            state = {
                items: [],
                loaded:false
            };
        }

        return state;
    }
}

export class EmployeeTableProps {
    filter: string | undefined;
}

class EmployeeTableState {
    items: IEmployeeDto[];
    loaded: boolean;
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