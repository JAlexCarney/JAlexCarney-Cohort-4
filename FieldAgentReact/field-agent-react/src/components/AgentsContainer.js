import React, {useEffect, useState} from 'react';
import AgentsTable from './AgentsTable';
import AgentFormSelector from './AgentFormSelector';

let Component = (props) => 
{
    const [state, setState] = useState({
        list:[],
        currentForm:"",
        token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE2MjIyMzUyMjgsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6MjAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6MjAwMCJ9.KravefU1H6I3QzdJCOCH-wkr1VT2rc_zR8wCKJJ65pQ",
        currentAgent:{}
    });

    useEffect(() => {
        const init = {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + state.token
            },
            redirect: 'follow'
        };
        fetch("https://localhost:44355/api/agents", init)
            .then(response => {
                if (response.status !== 200) {
                    return Promise.reject("agents fetch failed")
                }
                return response.json();
            })
            .then(json => {console.log(json); setState({list:json, currentForm:"", currentAgent:{}, token:state.token});})
            .catch(console.log);
    }, []);

    let viewAddForm = () => 
    {
        setState(
            {
                token:state.token,
                currentAgent:{},
                currentForm:"Add",
                list:state.list
            }
        );
    }

    let viewUpdateForm = (agent) => 
    {
        setState(
            {
                token:state.token,
                currentAgent:agent,
                currentForm:"Edit",
                list:state.list
            }
        );
    }

    let viewViewForm = (agent) => 
    {
        setState(
            {
                token:state.token,
                currentAgent:agent,
                currentForm:"View",
                list:state.list
            }
        );
    }

    let viewDeleteForm = (agent) => 
    {
        setState(
            {
                token:state.token,
                currentAgent:agent,
                currentForm:"Delete",
                list:state.list
            }
        );
    }

    let exitView = () => 
    {
        setState(
            {
                token:state.token,
                currentAgent:{},
                currentForm:"",
                list:state.list
            }
        );
    }

    let tableSize="col col-8";
    if(state.currentForm === "")
    {
        tableSize="col col-12";
    }

    let selectedAction = () => {};
    switch(state.currentForm)
    {
        case "Add":
            break;
        case "Edit":
            break;
        case "View":
            selectedAction = exitView;
            break;
        case "Delete":
            break;
        default:
            break;
    }

    return (
        <div className="container">
            <div className="row">
                <div className={tableSize}>
                    <AgentsTable 
                    list={state.list} handleAdd={viewAddForm} 
                    handleUpdate={viewUpdateForm} 
                    handleDelete={viewDeleteForm} 
                    handleView={viewViewForm}/>
                </div>
                <div className="col col-4">
                    <AgentFormSelector form={state.currentForm} agent={state.currentAgent} action={selectedAction}/>
                </div>
            </div>
        </div>
    );
}
export default Component;