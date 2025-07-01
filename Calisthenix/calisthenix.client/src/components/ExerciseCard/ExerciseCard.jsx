import React, { useRef, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { getUsername } from '../../utils/auth';
import './ExerciseCard.css';

function ExerciseCard({ exercise, onAdd, onRemove, onDelete, showAddButton = true, workouts = [], highlight = false }) {
    const navigate = useNavigate();
    const cardRef = useRef(null);
    const isOwner = exercise.userName === getUsername();

    const handleAddClick = async (e, workoutId) => {
        e.stopPropagation();
        if (!workoutId) return;

        const token = localStorage.getItem('token');
        const res = await fetch(`https://localhost:7161/api/workout/${workoutId}/exercise/${exercise.id}`, {
            method: 'POST',
            headers: { 'Authorization': `Bearer ${token}` }
        });

        if (res.ok) {
            onAdd?.(exercise.id);
        } else {
            alert('Failed to add exercise to workout');
        }
    };

    const goToDetails = () => {
        navigate(`/exercise/${exercise.id}`, { state: { exercise } });
    };

    const handleDelete = async () => {

        const token = localStorage.getItem('token');
        try {
            const res = await fetch(`https://localhost:7161/api/exercise/${exercise.id}`, {
                method: 'DELETE',
                headers: { 'Authorization': `Bearer ${token}` }
            });

            if (res.ok) {
                onDelete?.(exercise.id);
            } else {
                const errorText = await res.text();
                alert("Error: " + (errorText || "Something went wrong."));
            }
        } catch (err) {
            console.error(err);
            alert("Network error: " + err.message);
        }
    };

    useEffect(() => {
        if (highlight && cardRef.current) {
            cardRef.current.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
    }, [highlight]);

    return (
        <div
            ref={cardRef}
            className={`exercise-card ${highlight ? 'highlight' : ''}`}
            onClick={goToDetails}
        >
            <img src={exercise.imageUrl || 'https://via.placeholder.com/150'} alt={exercise.name} />
            <h3>{exercise.name}</h3>
            <p><strong>Category:</strong> {exercise.category}</p>
            <p><strong>Difficulty:</strong> {exercise.difficulty}</p>
            <p><strong>Equipment:</strong> {exercise.equipment}</p>

            {showAddButton && (
                workouts.length > 0 ? (
                    <select
                        className="workout-select"
                        onClick={(e) => e.stopPropagation()}
                        onChange={(e) => handleAddClick(e, e.target.value)}
                    >
                        <option value="">Add to Workout</option>
                        {workouts.map((w) => (
                            <option key={w.id} value={w.id}>{w.name}</option>
                        ))}
                    </select>
                ) : (
                    <p className="no-workouts">You have no workouts yet</p>
                )
            )}

            {onRemove && (
                <button
                    className="remove-button"
                    onClick={(e) => {
                        e.stopPropagation();
                        onRemove(exercise.id);
                    }}
                >
                    Remove
                </button>
            )}

            {isOwner && (
                <button
                    className="delete-button"
                    onClick={(e) => {
                        e.stopPropagation();
                        handleDelete();
                    }}
                >
                    Delete
                </button>
            )}
        </div>
    );
}

export default ExerciseCard;