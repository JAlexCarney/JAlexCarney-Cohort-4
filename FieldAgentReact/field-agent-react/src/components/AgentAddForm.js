import React from 'react';

let Component = (props) => 
{
    return (
        <div>
            <h3 className="form-header">Adding Agent</h3>
        <form>
            <div className="form-field">
                <label >First Name</label>
                <input type="text"></input>
            </div>
            <div className="form-field">
                <label>Last Name</label>
                <input type="text"></input>
            </div>
            <div className="form-field">
                <label>Date Of Birth</label>
                <input type="date"></input>
            </div>
            <button className="btn btn-primary btn-submit" type="submit">Confirm Add</button>
        </form>
        </div>
    );
}
export default Component;