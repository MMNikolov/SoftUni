import React, { useEffect, useState } from 'react';
import ExerciseCard from '../ExerciseCard/ExerciseCard';
import './AllExercises.css';

function AllExercises() {
    const [exercises, setExercises] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            const token = localStorage.getItem('token');
            const headers = { 'Authorization': `Bearer ${token}` };

            try {
                const [exRes, workoutRes] = await Promise.all([
                    fetch('https://localhost:7161/api/exercise', { headers }),
                    fetch('https://localhost:7161/api/workout', { headers })
                ]);

                if (!exRes.ok || !workoutRes.ok) {
                    const errorText = !exRes.ok
                        ? await exRes.text()
                        : await workoutRes.text();

                    console.error('API Error:', errorText);
                    throw new Error('Failed to fetch exercises or workouts.');
                }

                const exData = await exRes.json();
                const workoutData = await workoutRes.json();

                const allExercises = exData.$values || exData;
                const workoutList = workoutData.$values || workoutData;

                const workoutExerciseIds = workoutList.flatMap(w => {
                    const exercises = w.workoutExercises?.$values || [];
                    return exercises.map(e => e.exerciseId);
                });

                const filtered = allExercises.filter(e => !workoutExerciseIds.includes(e.id));
                setExercises(filtered);
            } catch (err) {
                console.error('Error fetching data:', err.message);
                alert('Could not load exercises. Try again later.');
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, []);

    const handleAddToWorkout = (exerciseId) => {
        setExercises(prev => prev.filter(e => e.id !== exerciseId));
    };

    if (loading) return <p>Loading exercises...</p>;

    return (
        <div className="home-container">
            <h1>All Calisthenics Exercises</h1>
            <div className="exercise-list">
                {exercises.length === 0 ? (
                    <p>No available exercises to add.</p>
                ) : (
                    exercises.map((exercise) => (
                        <ExerciseCard
                            key={exercise.id}
                            exercise={exercise}
                            onAdd={handleAddToWorkout}
                        />
                    ))
                )}
            </div>
        </div>
    );
}

export default AllExercises;
