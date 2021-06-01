import React from 'react';

let Component = (props) => 
{
    let mapToTr = (list) =>
    {
        return list.map((agent, i) => {
            return (
                <tr key={i}>
                    <td><button className="btn btn-secondary btn-round table-data" onClick={() => props.handleView(agent)}>{agent.agentId}</button></td>
                    <td className="table-data">{agent.lastName + ", " + agent.firstName}</td>
                    <td className="table-data">{agent.dateOfBirth.slice(0, 10)}</td>
                    <td><button className="btn btn-primary table-data btn-edit" onClick={() => props.handleUpdate(agent)}>Edit</button>
                    <button className="btn btn-danger table-data btn-delete" onClick={() => props.handleDelete(agent)}>Delete</button></td>
                </tr>
            );
        });
    }
    return (
        <div className="table-wrapper-scroll-y my-custom-scrollbar">
            <table className="table table-striped">
                <thead className="thead">
                    <tr>
                        <th>ID</th>
                        <th>Name</th>
                        <th>Date Of Birth</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    <tr key={-1}>
                        <td className="table-data">-</td>
                        <td className="table-data">-</td>
                        <td className="table-data">-</td>
                        <td><button className="btn btn-primary table-data btn-add" onClick={() => props.handleAdd()}>Add Agent</button></td>
                    </tr>
                    {mapToTr(props.list)}
                </tbody>
            </table>
        </div>
    );
}
export default Component;