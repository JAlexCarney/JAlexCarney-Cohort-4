import React from 'react';

let Component = (props) => 
{
    return (
        <div>
            <h3 className="form-header">Editing Agent{" " + props.agent.agentId}</h3>
        <form>
            <div className="form-field">
                <label>First Name</label>
                <input type="text" defaultValue={props.agent.firstName}></input>
            </div>
            <div className="form-field">
                <label>Last Name</label>
                <input type="text" defaultValue={props.agent.lastName}></input>
            </div>
            <div className="form-field">
                <label>Date Of Birth</label>
                <input type="date" defaultValue={props.agent.dateOfBirth}></input>
            </div>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Edit</button>
        </form>
        </div>
    );
}
export default Component;