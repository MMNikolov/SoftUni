import React, { useEffect, useState } from 'react';
import ExerciseCard from '../src/components/ExerciseCard/ExerciseCard'
import './App.css';

function App() {
    const [exercises, setExercises] = useState([]);

    useEffect(() => {
        fetch('https://localhost:7161/api/exercise') 
            .then((res) => res.json())
            .then((data) => setExercises(data))
            .catch((err) => console.error('Error fetching exercises:', err));
    }, []);

    return (
        <div className="home-container">
            <h1>All Calisthenics Exercises</h1>
            <div className="exercise-list">
                {exercises.map((exercise) => (
                    <ExerciseCard key={exercise.id} exercise={exercise} />
                ))}
            </div>
        </div>
    );
}

export default App;