import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './AddWorkout.css';

const AddWorkout = () => {
    const navigate = useNavigate();
    const [errors, setErrors] = useState({});
    const [formData, setFormData] = useState({
        name: '',
        category: '',
        equipment: '',
        difficulty: '',
        description: '',
        imageUrl: '',
        videoUrl: '',
    });

    const validate = () => {
        const newErrors = {};

        if (!formData.name.trim()) newErrors.name = "Name is required.";
        if (!formData.category) newErrors.category = "Please select a category.";
        if (!formData.equipment.trim()) newErrors.equipment = "Equipment is required.";
        if (!formData.difficulty) newErrors.difficulty = "Please select a difficulty level.";
        if (!formData.description.trim()) newErrors.description = "Description is required.";

        return newErrors;
    };

    const handleChange = (e) => {
        const { name, value } = e.target;

        setFormData(prev => ({ ...prev, [name]: value }));

        setErrors(prev => {
            const updated = { ...prev };
            if (value.trim() !== '' || name === 'category' || name === 'difficulty') {
                delete updated[name];
            }
            return updated;
        });
    };

    const handleSubmit = (e) => {
        e.preventDefault();

        const newErrors = validate();

        if (Object.keys(newErrors).length > 0) {
            setErrors(newErrors);
            return;
        }

        setErrors({});

        const token = localStorage.getItem('token');

        fetch('https://localhost:7161/api/exercise', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify(formData)
        })
            .then(async res => {
                if (res.ok) {
                    const data = await res.json(); 
                    localStorage.setItem('highlightExerciseId', data.id);
                    navigate('/exercises');
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
                <h2>Add New Exercise</h2>
                <form onSubmit={handleSubmit}>
                    <input
                        name="name"
                        placeholder="Name"
                        value={formData.name}
                        onChange={handleChange}
                        className={errors.name ? 'input-error' : ''}
                    />
                    {errors.name && <p className="error">{errors.name}</p>}
                    <select
                        name="category"
                        value={formData.category}
                        onChange={handleChange}
                        className={errors.category ? 'input-error' : ''}
                    >
                        <option value="">Select category</option>
                        <option value="Push">Push</option>
                        <option value="Pull">Pull</option>
                        <option value="Core">Core</option>
                        <option value="Legs">Legs</option>
                    </select>
                    {errors.category && <p className="error">{errors.category}</p>}
                    <input
                        name="equipment"
                        placeholder="Equipment"
                        value={formData.equipment}
                        onChange={handleChange}
                        className={errors.equipment ? 'input-error' : ''}
                    />
                    {errors.equipment && <p className="error">{errors.equipment}</p>}
                    <select
                        name="difficulty"
                        value={formData.difficulty}
                        onChange={handleChange}
                        className={errors.difficulty ? 'input-error' : ''}
                    >
                        <option value="">Select difficulty</option>
                        <option value="Beginner">Beginner</option>
                        <option value="Intermediate">Intermediate</option>
                        <option value="Advanced">Advanced</option>
                        <option value="Expert">Expert</option>
                    </select>
                    {errors.difficulty && <p className="error">{errors.difficulty}</p>}
                    <textarea
                        name="description"
                        placeholder="Description"
                        value={formData.description}
                        onChange={handleChange}
                        className={errors.difficulty ? 'input-error' : ''}
                    />
                    {errors.description && <p className="error">{errors.description}</p>}
                    <input name="imageUrl" placeholder="Image URL" onChange={handleChange} />
                    <input name="videoUrl" placeholder="Video URL" onChange={handleChange} />
                    <button type="submit">Add Exercise</button>
                </form>
            </div>
        </div>
    );
};

export default AddWorkout;