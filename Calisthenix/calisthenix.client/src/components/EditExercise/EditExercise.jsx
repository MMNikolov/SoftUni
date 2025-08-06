import React, { useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import './EditExercise.css';

function EditExercise() {
    const { state } = useLocation();
    const navigate = useNavigate();
    const [formData, setFormData] = useState({ ...state.exercise });

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const token = localStorage.getItem('token');
        const res = await fetch(`https://localhost:7161/api/exercise/${formData.id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(formData)
        });

        if (res.ok) {
            navigate('/exercises'); // change if route is different
        } else {
            alert('Failed to update exercise');
        }
    };

    return (
        <div className="edit-exercise-page">
            <h2>Edit Exercise</h2>
            <form onSubmit={handleSubmit} className="edit-exercise-form">
                <div>
                    <label htmlFor="name">Name</label>
                    <input type="text" name="name" value={formData.name} onChange={handleChange} required />
                </div>
                <div>
                    <label htmlFor="category">Category</label>
                    <input type="text" name="category" value={formData.category} onChange={handleChange} />
                </div>
                <div>
                    <label htmlFor="equipment">Equipment</label>
                    <input type="text" name="equipment" value={formData.equipment} onChange={handleChange} />
                </div>
                <div>
                    <label htmlFor="difficulty">Difficulty</label>
                    <input type="text" name="difficulty" value={formData.difficulty} onChange={handleChange} />
                </div>
                <div>
                    <label htmlFor="imageUrl">Image URL</label>
                    <input type="text" name="imageUrl" value={formData.imageUrl} onChange={handleChange} />
                </div>
                <div>
                    <label htmlFor="videoUrl">Video URL</label>
                    <input type="text" name="videoUrl" value={formData.videoUrl} onChange={handleChange} />
                </div>
                <div>
                    <label htmlFor="description">Description</label>
                    <textarea name="description" value={formData.description} onChange={handleChange} required />
                </div>

                <div className="form-actions">
                    <button type="submit">Save</button>
                    <button type="button" onClick={() => navigate(-1)}>Cancel</button>
                </div>
            </form>

        </div>
    );
}

export default EditExercise;
