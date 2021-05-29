import React, {useState} from 'react';
import AgentsContainer from './AgentsContainer';

let Component = (props) => 
{
    const [token, setToken] = useState("");
    const [state, setState] = useState({});

    const handleLogin = () => {
        const init = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
                "Accept": "application/json"
            },
            body: JSON.stringify(state)
        };
        fetch("https://localhost:44355/api/auth/login", init)
            .then(response => {
                if (response.status !== 200) {
                    return Promise.reject("login fetch failed")
                }
                return response.json();
            })
            .then(json => {setToken(json.token)})
            .catch(console.log);
    }

    const handleChange = (event) => {
        let newState = { ...state };

        newState[event.target.name] = event.target.value;

        setState(newState);
    };

    if(token === "")
    {
        return (
            <div className="container">
                <div className="row">
                    <div className="col col-4"></div>
                    <div className="col col-4">
                        <h3 className="form-header">Login</h3>
                        <form onSubmit={(event) => {event.preventDefault(); handleLogin();}}>
                            <div>
                                <label htmlFor="username">Username</label>
                                <input type="text" name="username" onChange={handleChange}></input>
                            </div>
                            <div>
                                <label htmlFor="password">Password</label>
                                <input type="password" name="password" onChange={handleChange}></input>
                            </div>
                            <button className="btn btn-primary btn-submit" type="submit">Login</button>
                        </form>
                    </div>
                    <div className="col col-4"></div>
                </div>
            </div>
        );
    }
    else
    {
        return <AgentsContainer token={token}/>
    }
    
}
export default Component;
