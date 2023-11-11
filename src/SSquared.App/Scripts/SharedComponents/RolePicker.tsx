
import React, { ChangeEvent } from "react";
import { IRoleDto } from "../DTO/IRoleDto";
import { getRoles } from "../DataAccess/RoleDataAccess";

export class RolePicker extends React.Component<RolePickerProps, RolePickerState> {
    constructor(p: RolePickerProps) {
        super(p);

        this.getState = this.getState.bind(this);
        this.itemToggled = this.itemToggled.bind(this);
    }

    public render() {
        var state = this.getState();
        var items = state.allRoles.map(role => <RolePickerItem role={role} roleToggled={this.itemToggled} selected={state.selectedRoleIds.includes(role.id)} ></RolePickerItem>);

        return <div>
            {items}
        </div>;
        
    }

    async componentDidMount() {
        var state = this.getState();
        state.allRoles = await getRoles();
        this.setState(state);
    }

    getState(): RolePickerState {
        var state = this.state;
        if (!state) {
            state = {
                allRoles: [],
                selectedRoleIds:[]
            };
        }

        return state;
    }

    itemToggled(roleId: number, selected: boolean) {
        var state = this.getState();

        if (selected && !state.selectedRoleIds.includes(roleId)) {
            state.selectedRoleIds.push(roleId);
            this.setState(state);
        }
        else if (!selected && state.selectedRoleIds.includes(roleId)) {
            var index = state.selectedRoleIds.indexOf(roleId);
            state.selectedRoleIds.splice(index, 1);
            this.setState(state);
        }

        this.props.itemsChanged(state.selectedRoleIds);
    }
}

export class RolePickerProps {
    itemsChanged: (selectedRoleIds: number[]) => void;
}

class RolePickerState {
    allRoles: IRoleDto[];
    selectedRoleIds: number[];
}

class RolePickerItem extends React.Component<RolePickerItemProps, RolePickerItemState> {
    constructor(p: RolePickerItemProps) {
        super(p);

        this.getState = this.getState.bind(this);
        this.roleToggled = this.roleToggled.bind(this);
    }

    public render() {
        var role = this.props.role;
        var checked = this.props.selected;

        var checkboxElement = (checked
            ? <input className="form-check-input" type="checkbox" onChange={this.roleToggled} value={role.id} checked ></input>
            : <input className="form-check-input" type="checkbox" onChange={this.roleToggled} value={role.id}></input>);

        return <div className="form-check">
            {checkboxElement}
            <label className="form-check-label">{role.name}</label>
        </div>
    }

    getState(): RolePickerItemState {
        var state = this.state;
        if (!state) {
            state = {
                selected:this.props.selected
            };
        }

        return state;
    }

    roleToggled(e:ChangeEvent) {
        var element = $(e.target);
        var roleId = this.props.role.id;
        var selected = element.is(":checked");

        this.props.roleToggled(roleId, selected);
    }
}

class RolePickerItemProps {
    role: IRoleDto;
    roleToggled: (roleId: number, selected: boolean) => void;
    selected:boolean
}

class RolePickerItemState {
    selected: boolean;
}