import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar/Navbar';
import AllExercises from './components/AllExercises/AllExercises';
import Home from './components/Home/Home';
import './App.css';

function App() {
    return (
        <Router>
            <div className="app">
                <Navbar />
                <main>
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/exercises" element={<AllExercises />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
}

export default App;