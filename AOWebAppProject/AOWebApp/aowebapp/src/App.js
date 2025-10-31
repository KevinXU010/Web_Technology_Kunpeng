
import './App.css';
import CardList from './components/CardList';
import CardListSearch from './components/CardListSearch';
import {Link, Outlet } from "react-router-dom"


function App() {
  return (
      <div className="App container">
          <nav className="navbar navbar-expand-lg navbar-light bg-light">
              <div className="conetainer-fluid">
                  <Link className="navbar-brand" to="/">AOWebApp</Link>
                  <button className="navbar-troggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNavAltMarkup">
                      <span className="navbar-troggler-icon"></span>
                  </button>
                  <div className="collapse navbar-collapse" id="navbarNavAltMarkup">
                      <div className="navbar-nav">
                          <Link className="nav-link active" to="/Home">Home</Link>
                          <Link className="nav-link active" to="/Contact">Contact</Link>
                          <Link className="nav-link active" to="/Products">Products</Link>
                          <Link className="nav-link active" to="/Graph">Graph</Link>
                            

                      </div>
                        
                  </div>
                
              </div>
                
          </nav>
          <Outlet />
    </div>
  );
}

export default App;
