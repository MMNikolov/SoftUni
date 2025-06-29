import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { getUsername, isLoggedIn, isAuthenticated } from '../../utils/auth';
import LogoutPage from '../LogoutPage/LogoutPage'; 
import './Navbar.css';

const Navbar = () => {
    const [username, setUsername] = useState(null);
    const [showModal, setShowModal] = useState(false);

    useEffect(() => {
        if (isLoggedIn()) {
            setUsername(getUsername());
        }
    }, []);

    const handleLogout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('username');
        window.location.href = '/login';
    };

    return (
        <nav className="navbar">
            <div className="navbar-logo">
                <Link to="/">Calisthenix</Link>
            </div>
            <ul className="navbar-links">
                <li><Link to="/">Home</Link></li>
                <li><Link to="/exercises">All Exercises</Link></li>
                {isAuthenticated() && <li><Link to="/add-workout">Add Workout</Link></li>}
                {isAuthenticated() && <li><Link to="/my-workouts">My Workouts</Link></li>}
                {!isLoggedIn() && <li><Link to="/login">Login</Link></li>}
                {!isLoggedIn() && <li><Link to="/register">Register</Link></li>}
                {isLoggedIn() && (
                    <li>
                        <button className="logout-button" onClick={() => setShowModal(true)}>
                            Logout
                        </button>
                    </li>
                )}
            </ul>
            {isLoggedIn() && (
                <div className="navbar-user">Greetings, {username}!</div>
            )}

            {showModal && (
                <LogoutPage
                    onConfirm={handleLogout}
                    onCancel={() => setShowModal(false)}
                />
            )}
        </nav>
    );
};

export default Navbar;