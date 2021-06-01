import React from 'react';
import './App.css';
import LoginForm from './components/LoginForm';

function App() {
  return (
    <div>
      <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark border-bottom box-shadow mb-3">
          <div className="container">
            <div className="row">
              <div className="col col-3">
                <img className="img-fluid header-img" src="CustomLogo.svg" alt="Logo"></img>
              </div>
              <div className="col col-9">
                <span className="navbar-brand">FieldAgent.React</span>
              </div>
            </div>
          </div>
        </nav>
      </header>
      <LoginForm></LoginForm>
    </div>
  );
}

export default App;
