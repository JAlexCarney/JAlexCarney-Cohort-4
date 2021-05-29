import React from 'react';

let Component = (props) => 
{
    return (
        <div>
            <h3 className="form-header">Viewing Agent{" " + props.agent.agentId}</h3>
            <table className="table table-striped">
                <tbody>
                    <tr>
                        <th>First Name</th>
                        <td>{props.agent.firstName}</td>
                    </tr>
                    <tr>
                        <th>Last Name</th>
                        <td>{props.agent.lastName}</td>
                    </tr>
                    <tr>
                        <th>Date Of Birth</th>
                        <td>{props.agent.dateOfBirth.slice(0, 10)}</td>
                    </tr>
                    <tr>
                        <th>Height</th>
                        <td>{props.agent.height}</td>
                    </tr>
                </tbody>
            </table>
            <form onSubmit={(event) => {event.preventDefault(); props.exitView();}}>
                <button className="btn btn-secondary btn-submit" type="submit">Stop Viewing</button>
            </form>
        </div>
    );
}
export default Component;