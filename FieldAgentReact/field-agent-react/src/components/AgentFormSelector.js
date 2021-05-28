import React from 'react';
import AgentAddForm from './AgentAddForm';
import AgentEditForm from './AgentEditForm';
import AgentViewForm from './AgentViewForm';
import AgentDeleteForm from './AgentDeleteForm';

let Component = (props) => 
{
    switch(props.form)
    {
        case "Add":
            return (<AgentAddForm handleAdd={props.action}/>);
        case "Edit":
            return (<AgentEditForm agent={props.agent} handleEdit={props.action}/>);
        case "View":
            return (<AgentViewForm agent={props.agent} exitView={props.action}/>);
        case "Delete":
            return (<AgentDeleteForm agent={props.agent} handleDelete={props.action}/>);
        default:
            return (<div></div>);
    }
}
export default Component;