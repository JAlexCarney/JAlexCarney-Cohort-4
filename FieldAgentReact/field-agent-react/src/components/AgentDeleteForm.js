import React from 'react';

let Component = (props) => 
{
    return (
        <div>
            <h3 className="form-header">Deleting Agent{" " + props.agent.agentId}</h3>
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
                        <td>{props.agent.dateOfBirth}</td>
                    </tr>
                </tbody>
            </table>
            <form>
                <button className="btn btn-danger btn-submit" type="submit">Confirm Delete</button>
            </form>
        </div>
    );
}
export default Component;