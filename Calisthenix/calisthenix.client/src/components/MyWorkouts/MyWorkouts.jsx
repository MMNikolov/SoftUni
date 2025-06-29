import React, { useEffect, useState } from 'react';
import ExerciseCard from '../ExerciseCard/ExerciseCard';
import '../AllExercises/AllExercises.css'; // reuse styling

const MyWorkouts = () => {
    const [exercises, setExercises] = useState([]);

    useEffect(() => {
        const token = localStorage.getItem('token');

        fetch('https://localhost:7161/api/exercise/mine', {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
            .then(res => res.json())
            .then(data => setExercises(data))
            .catch(err => console.error('Error fetching my workouts:', err));
    }, []);

    return (
        <div className="home-container">
            <h1>My Workouts</h1>
            <div className="exercise-list">
                {exercises.map((exercise) => (
                    <ExerciseCard key={exercise.id} exercise={exercise} />
                ))}
            </div>
        </div>
    );
};

export default MyWorkouts;