import React from 'react';
import { useNavigate } from 'react-router-dom';
import './ExerciseCard.css';

function ExerciseCard({ exercise, onAdd, showAddButton = true }) {
    const navigate = useNavigate();

    const handleAddClick = async (e) => {
        e.stopPropagation();
        const token = localStorage.getItem('token');
        const res = await fetch(`https://localhost:7161/api/workout/add/${exercise.id}`, {
            method: 'POST',
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });

        if (res.ok) {
            onAdd?.(exercise.id);
        } else {
            alert('Failed to add exercise to workout');
        }
    };

    const goToDetails = () => {
        navigate(`/exercise/${exercise.id}`, { state: { exercise } });
    }

    return (
        <div className="exercise-card" onClick={ goToDetails }>
            <img src={exercise.imageUrl || 'https://via.placeholder.com/150'} alt={exercise.name} />
            <h3>{exercise.name}</h3>
            <p><strong>Category:</strong> {exercise.category}</p>
            <p><strong>Difficulty:</strong> {exercise.difficulty}</p>
            <p><strong>Equipment:</strong> {exercise.equipment}</p>

            {showAddButton && (
                <button onClick={handleAddClick}>Add to My Workout</button>
            )}
        </div>
    );
}

export default ExerciseCard;
