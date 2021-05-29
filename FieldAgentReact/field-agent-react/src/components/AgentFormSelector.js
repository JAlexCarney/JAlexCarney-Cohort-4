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
            return (<AgentAddForm handleAdd={props.action} exitView={props.exitAction}/>);
        case "Edit":
            return (<AgentEditForm agent={props.agent} handleEdit={props.action} exitView={props.exitAction}/>);
        case "View":
            return (<AgentViewForm agent={props.agent} exitView={props.exitAction}/>);
        case "Delete":
            return (<AgentDeleteForm agent={props.agent} handleDelete={props.action} exitView={props.exitAction}/>);
        default:
            return (<div></div>);
    }
}
export default Component;