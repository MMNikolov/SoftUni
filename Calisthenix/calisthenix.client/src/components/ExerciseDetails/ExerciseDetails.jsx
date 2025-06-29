import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import './ExerciseDetails.css';

const ExerciseDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [exercise, setExercise] = useState(null);

    useEffect(() => {
        fetch(`https://localhost:7161/api/exercise/${id}`)
            .then(res => res.json())
            .then(data => setExercise(data))
            .catch(err => console.error('Error fetching exercise:', err));
    }, [id]);

    if (!exercise) return <div>Loading...</div>;

    return (
        <div className="exercise-details">
            <h1>{exercise.name}</h1>
            <img src={exercise.imageUrl} alt={exercise.name} />
            <p><strong>Category:</strong> {exercise.category}</p>
            <p><strong>Equipment:</strong> {exercise.equipment}</p>
            <p><strong>Difficulty:</strong> {exercise.difficulty}</p>
            <p><strong>Description:</strong> {exercise.description}</p>
            {exercise.videoUrl && (
                <a href={exercise.videoUrl} target="_blank" rel="noreferrer">
                    Watch Tutorial
                </a>
            )}
            <button className="back-button" onClick={() => navigate(-1)}>&larr; Back</button>
        </div>
    );
};

export default ExerciseDetails;