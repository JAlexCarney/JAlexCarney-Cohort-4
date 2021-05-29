import React, {useState} from 'react';

let Component = (props) => 
{
    const [state, setState] = useState({});

    const handleChange = (event) => {
        let newState = { ...state };

        newState[event.target.name] = event.target.value;

        setState(newState);
    };

    return (
        <div>
            <h3 className="form-header">Adding Agent</h3>
        <form onSubmit={(event) => {event.preventDefault(); props.handleAdd(state);}}>
            <div className="form-field">
                <label htmlFor="firstName">First Name</label>
                <input type="text" name="firstName" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="lastName">Last Name</label>
                <input type="text" name="lastName" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="dateOfBirth">Date Of Birth</label>
                <input type="date" name="dateOfBirth" onChange={handleChange}></input>
            </div>
            <div className="form-field">
                <label htmlFor="height">Height</label>
                <input type="number" step="0.1" min="0" max="10" name="height" onChange={handleChange}></input>
            </div>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Add</button><br/>
            <button className="btn btn-secondary btn-submit" onClick={props.exitView}>Cancel</button>
        </form>
        </div>
    );
}
export default Component;