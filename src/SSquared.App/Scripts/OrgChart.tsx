import React from "react";
import { IOrgChartNode } from "./DTO/IOrgChartNode";
import { getEmployeeOrgChart } from "./DataAccess/EmployeeDataAccess";
import ReactDOM from "react-dom";

export async function init(employeeId: number) {
    var node = await getEmployeeOrgChart(employeeId);

    if (node) {
        var element = document.getElementsByClassName("react-wrapper")[0];
        ReactDOM.render(<OrgChartNodeComponent key="topOrgChartNode" node={ node} ></OrgChartNodeComponent>, element);
    }
}

class OrgChartNodeComponent extends React.Component<OrgChartNodeComponentProps, OrgChartNodeComponentState>{
    public render() {
        var node = this.props.node;
        var children = node.nodes.map(subNode => <OrgChartNodeComponent key={`subNode${subNode.id}`} node={subNode}></OrgChartNodeComponent>);

        return <div className="org-chart-node">
            <div className="org-chart-node-details">
                <span>{node.firstName} {node.lastName}</span>
            </div>
            {children}
        </div>;
    }
}

class OrgChartNodeComponentProps {
    node:IOrgChartNode
} 

class OrgChartNodeComponentState {

}