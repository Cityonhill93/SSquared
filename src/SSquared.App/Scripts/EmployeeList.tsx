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
        const employeePickerId = "employeePicker";

        var state = this.getState();

        return <div>
            <div className="form-group"> 
                <label htmlFor={employeePickerId} >Manager</label>
                <EmployeePicker id={employeePickerId} key={employeePickerId} employeeSelected={this.managerSelected} ></EmployeePicker>
            </div>
            <EmployeeTable key="employeeTable" data={state.employees} ></EmployeeTable> 
        </div>;
    }

    async componentDidMount() {
        var state = this.getState();
        state.employees = await this.getData(null);
        this.setState(state);
    }

    async getData(managerId:number|null): Promise<IEmployeeDto[]> {

        if (managerId) {
            var employee = await getEmployee(managerId);

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
                employees: []
            };
        }

        return state;
    }

    async managerSelected(employeeId: number|null) {
        var state = this.getState();

        state.employees = await this.getData(employeeId);
        this.setState(state);
    }
}

class EmployeeListPageProps {

}

class EmployeeListPageState {
    employees: IEmployeeDto[];
}