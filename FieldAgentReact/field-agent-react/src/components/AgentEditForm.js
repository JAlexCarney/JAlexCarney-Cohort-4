import React, {useState, useEffect} from 'react';

let Component = (props) => 
{
    const [state, setState] = useState({});

    useEffect(() => {
        setState(props.agent);
    }, [props.agent]);

    const handleChange = (event) => {
        let newState = { ...state };

        newState[event.target.name] = event.target.value;

        setState(newState);
    };

    return (
        <div className="agent-form-container">
            <h3 className="form-header">Editing Agent{" " + props.agent.agentId}</h3>
        <form key={props.agent.agentId} onSubmit={(event) => {event.preventDefault(); props.handleEdit(state);}}>
            <div className="form-field">
                <label htmlFor="firstName">First Name</label>
                <input type="text" defaultValue={props.agent.firstName} name="firstName" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="lastName">Last Name</label>
                <input type="text" defaultValue={props.agent.lastName} name="lastName" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="dateOfBirth">Date Of Birth</label>
                <input type="date" defaultValue={props.agent.dateOfBirth.slice(0, 10)} name="dateOfBirth" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="height">Height</label>
                <input type="number" step="0.1" min="0" max="10" defaultValue={props.agent.height} name="height" onChange={handleChange}></input>
            </div>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Edit</button><br/>
            <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
        </form>
        </div>
    );
}
export default Component;