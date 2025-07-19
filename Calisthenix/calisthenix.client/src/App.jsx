import React from 'react';
import Home from './components/Home/Home';
import Navbar from './components/Navbar/Navbar';
import AllExercises from './components/AllExercises/AllExercises';
import AddWorkout from './components/AddWorkout/AddWorkout';
import Login from './components/Login/Login';
import Register from './components/Register/Register';
import MyWorkouts from './components/MyWorkouts/MyWorkouts';
import ExerciseDetails from './components/ExerciseDetails/ExerciseDetails';
import Profile from './components/Profile/Profile';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { isAuthenticated } from './utils/auth';
import { Navigate } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './App.css';

function App() {
    return (
        <Router>
            <div className="app">
                <Navbar />
                <ToastContainer
                    position="top-center"
                    autoClose={3000}
                    hideProgressBar={false}
                    newestOnTop={false}
                    closeOnClick
                    rtl={false}
                    pauseOnFocusLoss
                    draggable
                    pauseOnHover
                    theme="colored"
                />
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
                        <Route path="/profile" element={<Profile />} />
                    </Routes>
                </main>
            </div>
        </Router>
    );
}

export default App;