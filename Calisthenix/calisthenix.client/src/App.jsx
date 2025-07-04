import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Navbar from './components/Navbar/Navbar';
import AllExercises from './components/AllExercises/AllExercises';
import Home from './components/Home/Home';
import './App.css';
import AddWorkout from './components/AddWorkout/AddWorkout';
import Login from './components/Login/Login';
import Register from './components/Register/Register';
import MyWorkouts from './components/MyWorkouts/MyWorkouts';
import { isAuthenticated } from './utils/auth';
import { Navigate } from 'react-router-dom';
import ExerciseDetails from './components/ExerciseDetails/ExerciseDetails';

function App() {
    return (
        <Router>
            <div className="app">
                <Navbar />
                <main>
                    <Routes>
                        <Route path="/" element={<Home />} />
                        <Route path="/exercises" element={<AllExercises />} />
                        <Route
                            path="/add-workout"
                            element={isAuthenticated() ? <AddWorkout /> : <Navigate to="/login" />}
                        />
                        <Route path="/my-workouts" element={<MyWorkouts />} />
                        <Route path="/exercise/:id" element={<ExerciseDetails />} />
                        <Route path="/my-workouts" element={<MyWorkouts />} />
                        <Route path="/login" element={<Login />} />
                        <Route path="/register" element={<Register />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
}

export default App;