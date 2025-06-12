import React from 'react';
import './ExerciseCard.css';

function ExerciseCard({ exercise }) {
    return (
        <div className="exercise-card">
            <img src={exercise.imageUrl} alt={exercise.name} />
            <div className="exercise-info">
                <h2>{exercise.name}</h2>
                <p><strong>Category:</strong> {exercise.category}</p>
                <p><strong>Equipment:</strong> {exercise.equipment}</p>
                <p><strong>Difficulty:</strong> {exercise.difficulty}</p>
                <p>{exercise.description}</p>
                {exercise.videoUrl && (
                    <a href={exercise.videoUrl} target="_blank" rel="noreferrer">
                        Watch Tutorial
                    </a>
                )}
            </div>
        </div>
    );
}

export default ExerciseCard;