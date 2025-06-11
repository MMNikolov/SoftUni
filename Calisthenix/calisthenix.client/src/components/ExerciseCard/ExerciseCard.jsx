import React from 'react';
import './ExerciseCard.css';

function ExerciseCard({ exercise }) {
    return (
        <div className="exercise-card">
            <h2>{exercise.name}</h2>
            <p><strong>Category:</strong> {exercise.category}</p>
            <p><strong>Equipment:</strong> {exercise.equipment}</p>
            <p><strong>Difficulty:</strong> {exercise.difficulty}</p>
            <p>{exercise.description}</p>
            {exercise.imageUrl && <img src={exercise.imageUrl} alt={exercise.name} />}
        </div>
    );
}

export default ExerciseCard;