import React from 'react';
import { useNavigate } from 'react-router-dom';
import './ExerciseCard.css';

function ExerciseCard({ exercise }) {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(`/exercises/${exercise.id}`);
    };

    return (
        <div className="exercise-card" onClick={handleClick}>
            <img src={exercise.imageUrl} alt={exercise.name} />
            <div className="exercise-info">
                <h2>{exercise.name}</h2>
                <p><strong>Category:</strong> {exercise.category}</p>
                <p><strong>Equipment:</strong> {exercise.equipment}</p>
                <p><strong>Difficulty:</strong> {exercise.difficulty}</p>
            </div>
        </div>
    );
}

export default ExerciseCard;
