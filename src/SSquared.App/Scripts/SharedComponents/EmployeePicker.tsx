import * as React from 'react'
import { IEmployeeDto } from '../DTO/IEmployeeDto';
import { getEmployees } from '../DataAccess/EmployeeDataAccess';

export class EmployeePicker extends React.Component<EmployeePickerProps, EmployeePickerState> {
    constructor(p: EmployeePickerProps) {
        super(p);

        this.getState = this.getState.bind(this);
    }

    public render() {
        var state = this.getState();
        if (!state.loaded) {
            return null;
        }

        var items = state.items.map(i => <option key={ `EmployeeOption${i.id}`} value={i.id}>{i.firstName} { i.lastName}</option>);
        return <select key="employeePicker" className="form-select">
            { items}
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
}

export class EmployeePickerProps {

}

class EmployeePickerState {
    items: IEmployeeDto[];
    loaded: boolean;
}