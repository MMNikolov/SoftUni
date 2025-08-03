import React from 'react';
import { lazy, Suspense } from 'react';
import Navbar from './components/Navbar/Navbar';
import NotFound from './components/NotFound/NotFound';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { isAuthenticated } from './utils/auth';
import { Navigate } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import './App.css';

const Home = lazy(() => import('./components/Home/Home'));
const AllExercises = lazy(() => import('./components/AllExercises/AllExercises'));
const AddWorkout = lazy(() => import('./components/AddWorkout/AddWorkout'));
const Login = lazy(() => import('./components/Login/Login'));
const Register = lazy(() => import('./components/Register/Register'));
const MyWorkouts = lazy(() => import('./components/MyWorkouts/MyWorkouts'));
const ExerciseDetails = lazy(() => import('./components/ExerciseDetails/ExerciseDetails'));
const Profile = lazy(() => import('./components/Profile/Profile'));

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
                    <Suspense fallback={<div>Loading...</div>}>
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
                            <Route path="*" element={<NotFound />} />
                        </Routes>
                    </Suspense>
                </main>
            </div>
        </Router>
    );
}

export default App;