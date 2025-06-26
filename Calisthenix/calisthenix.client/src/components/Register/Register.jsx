import React, { useState } from 'react';
import { register } from '../../services/authService';
import './Register.css';

const Register = () => {
    const [formData, setFormData] = useState(
        {
            username: '',
            password: ''
        });

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) =>
    {
        e.preventDefault();
        try {
            const token = await register(formData.username, formData.password);

            localStorage.setItem('token', token);

            window.location.href = '/';
        }
        catch (err)
        {
            alert(err.message);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="auth-form">
            <h2>Register</h2>
            <input name="username" placeholder="Username" onChange={handleChange} required />
            <input type="password" name="password" placeholder="Password" onChange={handleChange} required />
            <button type="submit">Register</button>
        </form>
    );
};

export default Register;
