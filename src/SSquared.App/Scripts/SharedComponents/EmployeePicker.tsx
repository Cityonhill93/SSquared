﻿import * as React from 'react'
import { IEmployeeDto } from '../DTO/IEmployeeDto';
import { getEmployees } from '../DataAccess/EmployeeDataAccess';

export class EmployeePicker extends React.Component<EmployeePickerProps, EmployeePickerState> {
    constructor(p: EmployeePickerProps) {
        super(p);

        this.getState = this.getState.bind(this);
        this.valueChanged = this.valueChanged.bind(this)
    }

    public render() {
        var state = this.getState();
        if (!state.loaded) {
            return null;
        }

        var items = state.items.map(i => <option key={ `EmployeeOption${i.id}`} value={i.id}>{i.firstName} { i.lastName}</option>);
        return <select key="employeePicker" className="form-select" onChange={ this.valueChanged} >
            <option selected>Select an employee....</option>
            {items}
        </select>
    }

    async componentDidMount() {
        var state = this.getState();
        if (!state.loaded) {
            var employees = await getEmployees();
            state.loaded = true;
            state.items = employees;
            this.setState(state);
        }
    }

    getState(): EmployeePickerState {
        var state = this.state;
        if (!state) {
            state = {
                items: [],
                loaded: false
            };
        }

        return state;
    }

    valueChanged(e:React.ChangeEvent) {
        var select = $(e.target);
        var selectedValue = select.val();
        var numericValue = Number(selectedValue);
        if (!isNaN(numericValue)) {
            this.props.employeeSelected(numericValue);
        }
        else {
            this.props.employeeSelected(null);
        }
    }
}

export class EmployeePickerProps {
    employeeSelected: (employeeId: number | null) => void;
}

class EmployeePickerState {
    items: IEmployeeDto[];
    loaded: boolean;
}