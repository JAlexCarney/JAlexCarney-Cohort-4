import React from 'react';
import './App.css';
import LoginForm from './components/LoginForm';

function App() {
  return (
    <div>
      <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-dark border-bottom box-shadow mb-3">
          <div className="container">
            <span className="navbar-brand">FieldAgent.React</span>
          </div>
        </nav>
      </header>
      <LoginForm></LoginForm>
    </div>
  );
}

export default App;
