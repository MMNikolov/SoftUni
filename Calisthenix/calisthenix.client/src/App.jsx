import React from 'react';
import { lazy, Suspense } from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { isAuthenticated } from './utils/auth';
import { Navigate } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import Navbar from './components/Navbar/Navbar';
import NotFound from './components/NotFound/NotFound';
import EditExercise from './components/EditExercise/EditExercise';
import AdminDashboard from './components/Admin/AdminDashboard';
import RequireAdmin from './components/AuthGuards/RequireAdmin';
import 'react-toastify/dist/ReactToastify.css';
import './App.css';


function App() {
    const Home = lazy(() => import('./components/Home/Home'));
    const AllExercises = lazy(() => import('./components/AllExercises/AllExercises'));
    const AddWorkout = lazy(() => import('./components/AddWorkout/AddWorkout'));
    const Login = lazy(() => import('./components/Login/Login'));
    const Register = lazy(() => import('./components/Register/Register'));
    const MyWorkouts = lazy(() => import('./components/MyWorkouts/MyWorkouts'));
    const ExerciseDetails = lazy(() => import('./components/ExerciseDetails/ExerciseDetails'));
    const Profile = lazy(() => import('./components/Profile/Profile'));

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
                            <Route path="/exercise/edit/:id" element={<EditExercise />} />
                            <Route
                                path="/admin"
                                element={
                                    <RequireAdmin>
                                        <AdminDashboard />
                                    </RequireAdmin>
                                }
                            />
                            <Route path="/not-authorized" element={<h2>🚫 Not authorized</h2>} />
                            <Route path="*" element={<NotFound />} />
                        </Routes>
                    </Suspense>
                </main>
            </div>
        </Router>
    );
}

export default App;