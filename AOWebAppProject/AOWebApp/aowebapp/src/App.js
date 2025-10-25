
import './App.css';
import CardList from './components/CardList';


function App() {
  return (
      <div className="App container">
          <div className="bg-light py-1 mb-2">
              <h2 className = "text-center">Example Application</h2>
          </div>
          <div className="row justify-content-center">
              <CardList
              />
          </div>
    </div>
  );
}

export default App;
