import React, { useState } from 'react';
import './AddWorkout.css';

const AddWorkout = () => {
    const [formData, setFormData] = useState({
        name: '',
        category: '',
        equipment: '',
        difficulty: '',
        description: '',
        imageUrl: '',
        videoUrl: '',
        userId: 4
    });

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        fetch('https://localhost:7161/api/exercise', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(formData)
        })
            .then(async res => {
                if (res.ok)
                {
                    alert('Workout added!');
                }
                else
                {
                    const errorText = await res.text();
                    console.error('Backend error:', errorText);
                    alert('Failed to add! Check console.');
                }
            })
            .catch(err => {
                console.error('Catch error:', err);
                alert('Something went wrong!');
            });
    };

    return (
        <div className="container">
            <div className="add-workout-container">
                <h2>Add New Workout</h2>
                <form onSubmit={handleSubmit}>
                    <input name="name" placeholder="Name" onChange={handleChange} required />
                    <input name="category" placeholder="Category" onChange={handleChange} required />
                    <input name="equipment" placeholder="Equipment" onChange={handleChange} required />
                    <select name="difficulty" value={formData.difficulty} onChange={handleChange} required>
                        <option value="">Select difficulty</option>
                        <option value="Beginner">Beginner</option>
                        <option value="Intermediate">Intermediate</option>
                        <option value="Advanced">Advanced</option>
                        <option value="Advanced">Expert</option>
                    </select>
                    <textarea name="description" placeholder="Description" onChange={handleChange} required />
                    <input name="imageUrl" placeholder="Image URL" onChange={handleChange} />
                    <input name="videoUrl" placeholder="Video URL" onChange={handleChange} />
                    <button type="submit">Add Workout</button>
                </form>
            </div>
        </div>
    );
};

export default AddWorkout;