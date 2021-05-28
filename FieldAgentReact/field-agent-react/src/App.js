import React from 'react';
import './App.css';
import AgentsContainer from './components/AgentsContainer';

function App() {
  return (
    <div>
      <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark border-bottom box-shadow mb-3">
          <div className="container">
            <span className="navbar-brand">FieldAgent.React</span>
            <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul className="navbar-nav flex-grow-1">
                        <li className="nav-item">
                            <span className="nav-link text-light">Login</span>
                        </li>
                        <li className="nav-item">
                            <span className="nav-link text-light">Agent</span>
                        </li>
                    </ul>
                </div>
          </div>
        </nav>
      </header>
      <AgentsContainer></AgentsContainer>
    </div>
  );
}

export default App;
