import React from "react";
import ReactDOM from "react-dom";
import { EmployeePicker } from "./SharedComponents/EmployeePicker";
import { EmployeeTable } from "./SharedComponents/EmployeeTable";
import { IEmployeeDto } from "./DTO/IEmployeeDto";
import { getEmployee, getEmployees } from "./DataAccess/EmployeeDataAccess";


export function init() {
    var element = document.getElementsByClassName("react-wrapper")[0];
    ReactDOM.render(<EmployeeListPage></EmployeeListPage>, element);
}

class EmployeeListPage extends React.Component<EmployeeListPageProps, EmployeeListPageState> {
    constructor(p: EmployeeListPageProps) {
        super(p);

        this.getData = this.getData.bind(this);
        this.getState = this.getState.bind(this);
        this.managerSelected = this.managerSelected.bind(this);
    }

    public render() {       
        return <div>
            <EmployeePicker key="EmployeePicker" employeeSelected={ this.managerSelected} ></EmployeePicker>
            <EmployeeTable key="EmployeeTable" getData={this.getData} ></EmployeeTable> 
        </div>;
    }

    async getData(): Promise<IEmployeeDto[]> {
        var state = this.getState();

        var selectedManagerId = state.selectedManagerId;
        if (selectedManagerId) {
            var employee = await getEmployee(selectedManagerId);

            return employee.employees;
        }
        else {
            return await getEmployees();
        }
    }

    getState(): EmployeeListPageState {
        var state = this.state;
        if (!state) {
            state = {
                selectedManagerId:null
            };
        }

        return state;
    }

    managerSelected(employeeId: number) {
        var state = this.getState();
        state.selectedManagerId = employeeId;
        this.setState(state);
    }
}

class EmployeeListPageProps {

}

class EmployeeListPageState {
    selectedManagerId:number|undefined
}