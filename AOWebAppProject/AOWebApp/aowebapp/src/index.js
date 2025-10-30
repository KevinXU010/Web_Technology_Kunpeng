import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter, Routes, Route } from "react-router-dom"
import Home from "./components/routes/Home"
import Contact from "./components/routes/Contact"
import CardListSearch from './components/CardListSearch';




const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
    <React.StrictMode>
        <BrowserRouter>
            
            <Routes>
                <Route path="/" element={<App />} />
                <Route path="Home" element={<Home />} />
                <Route path="Contact" element={<Contact />} />
                <Route path="Products" element={<CardListSearch />} />
                <Route path=" " element={<Home />} />
                <Route path="*" element={<Home />} />
                
            </Routes>
        
      </BrowserRouter>
  </React.StrictMode>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
