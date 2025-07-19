import React, { useEffect, useState, useRef } from 'react';
import { Link } from 'react-router-dom';
import { getUsername, isLoggedIn, isAuthenticated } from '../../utils/auth';
import LogoutPage from '../LogoutPage/LogoutPage'; 
import './Navbar.css';

const Navbar = () => {
    const [username, setUsername] = useState(null);
    const [showModal, setShowModal] = useState(false);
    const [showDropdown, setShowDropdown] = useState(false);
    const dropdownRef = useRef(null);

    useEffect(() => {
        if (isLoggedIn()) {
            setUsername(getUsername());
        }

        const handleClickOutside = (event) => {
            if (dropdownRef.current && !dropdownRef.current.contains(event.target)) {
                setShowDropdown(false);
            }
        };

        document.addEventListener('mousedown', handleClickOutside);

        return () => {
            document.removeEventListener('mousedown', handleClickOutside);
        };
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
                {isAuthenticated() && <li><Link to="/add-workout">Add Exercise</Link></li>}
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
                <div className="profile-dropdown" ref={ dropdownRef }>
                    <button
                        className="user-badge"
                        onClick={() => setShowDropdown(!showDropdown)}
                    >
                        {username} {showDropdown ? '▲' : '▼'}
                    </button>

                    {showDropdown && (
                        <div className="dropdown-menu">
                            <Link to="/profile" onClick={() => setShowDropdown(false)}>Profile</Link>
                            <Link to="/my-workouts" onClick={() => setShowDropdown(false)}>My Workouts</Link>
                            <button onClick={() => { setShowDropdown(false); setShowModal(true); }}>Logout</button>
                        </div>
                    )}
                </div>
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