import React from "react";
import ReactDOM from "react-dom";
import { EmployeePicker } from "./SharedComponents/EmployeePicker";
import { EmployeeTable } from "./SharedComponents/EmployeeTable";


export function init() {
    var element = document.getElementsByClassName("react-wrapper")[0];
    ReactDOM.render(<EmployeeListPage></EmployeeListPage>, element);
}

class EmployeeListPage extends React.Component<EmployeeListPageProps, EmployeeListPageState> {
    public render() {
        //TODO:Get the filter from the employee picker and apply it to the table

        return <div>
            <EmployeePicker key="EmployeePicker"></EmployeePicker>
            <EmployeeTable key="EmployeeTable" filter={null } ></EmployeeTable> 
        </div>;
    }
}

class EmployeeListPageProps {

}

class EmployeeListPageState {

}