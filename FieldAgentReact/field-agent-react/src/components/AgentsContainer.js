import React, {useEffect, useState} from 'react';
import AgentsTable from './AgentsTable';
import AgentFormSelector from './AgentFormSelector';

let Component = (props) => 
{
    const [state, setState] = useState({
        list:[],
        currentForm:"",
        token: props.token,
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
            .then(json => {setState({list:json, currentForm:"", currentAgent:{}, token:state.token})})
            .catch(console.log);
    }, [state.token]);

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

    let handleAdd = (agent) => 
    {
        const init = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + state.token
            },
            body: JSON.stringify(agent)
          };
      
          fetch("https://localhost:44355/api/agents", init)
            .then(response => {
                if (response.status !== 201) {
                    return Promise.reject("response is not 200 OK");
                }
                return response.json();
            })
            .then(json => setState({
                list:[...state.list, json],
                currentForm:"",
                currentAgent:{},
                token:state.token
            }))
            .catch(console.log);
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

    let handleUpdate = (agent) => 
    {
        const init = {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json",
                "Authorization": "Bearer " + state.token
            },
            body: JSON.stringify(agent)
        };

        fetch("https://localhost:44355/api/agents", init)
            .then(response => {
                if (response.status !== 200) {
                    return Promise.reject("response is not 200 OK");
                }
            })
            .then(() => {
            let uneditedAgent = state.list.find(a => a.agentId === agent.agentId);
            let index = state.list.indexOf(uneditedAgent);
            let newList = [...state.list]
            newList[index] = agent;
            setState({
                list:newList,
                currentForm:"",
                currentAgent:{},
                token:state.token
            });})
            .catch(console.log);
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

    let handleDelete = (agent) =>
    {
        let id = agent.agentId;

        const init = {
            method: "DELETE",
            headers: {
                "Authorization": "Bearer " + state.token
            }
        };

        fetch(`https://localhost:44355/api/agents/${id}`, init)
            .then(response => {
                if (response.status === 204) {
                    // `filter` new state
                    setState({
                        list:state.list.filter(agent => agent.agentId !== id),
                        currentForm:"",
                        currentAgent:{},
                        token:state.token
                    });
                } else if (response.status === 404) {
                    return Promise.reject("agent not found");
                } else {
                    return Promise.reject(`Delete failed with status: ${response.status}`);
                }
            })
            .catch(console.log);
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
            selectedAction = handleAdd;
            break;
        case "Edit":
            selectedAction = handleUpdate;
            break;
        case "Delete":
            selectedAction = handleDelete;
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
                    <AgentFormSelector form={state.currentForm} agent={state.currentAgent} action={selectedAction} exitAction={exitView}/>
                </div>
            </div>
        </div>
    );
}
export default Component;