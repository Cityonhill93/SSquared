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
    constructor(p: OrgChartNodeComponentProps) {
        super(p);

        this.getBootstrapClassName = this.getBootstrapClassName.bind(this);
    }

    public render() {
        var node = this.props.node;
        var nodeLevel = this.props.nodeLevel ?? 0;
        var children = node.nodes.map(subNode => <OrgChartNodeComponent key={`subNode${subNode.id}`} node={subNode} nodeLevel={nodeLevel+1} ></OrgChartNodeComponent>);
        var childContainer = (children.length > 0
            ?   <div className="container org-chart-node-children">
                    <div className="row">
                        {children}
                    </div>
                </div>  
            : null);

        

        return <div className={`org-chart-node ${this.getBootstrapClassName()}`} >
            <div className="org-chart-node-details">
                <span>{node.firstName} {node.lastName}</span>
            </div>
            {childContainer}          
        </div>;
    }

    getBootstrapClassName(): string {
        var nodeLevel = this.props.nodeLevel;

        switch (nodeLevel) {
            case 0:
                return "";
            case 1:
                return "col-4";
            case 2:
                return "col-6";
            default:
                return "col-12";
        }
    }
}

class OrgChartNodeComponentProps {
    node: IOrgChartNode
    nodeLevel?: number;
} 

class OrgChartNodeComponentState {

}