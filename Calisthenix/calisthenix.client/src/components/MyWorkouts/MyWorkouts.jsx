import React, { useEffect, useState } from 'react';
import ExerciseCard from '../ExerciseCard/ExerciseCard';
import './MyWorkouts.css';

const MyWorkouts = () => {
    const [workouts, setWorkouts] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchWorkouts = async () => {
            const token = localStorage.getItem('token');
            if (!token) {
                alert('You must be logged in to view your workouts.');
                return;
            }

            try {
                const res = await fetch('https://localhost:7161/api/workout', {
                    headers: { 'Authorization': `Bearer ${token}` }
                });

                if (!res.ok) throw new Error('Failed to fetch workouts.');

                const data = await res.json();
                const workoutsList = data.$values || data;
                setWorkouts(workoutsList);
            } catch (err) {
                console.error(err);
                alert('Error loading workouts.');
            } finally {
                setLoading(false);
            }
        };

        fetchWorkouts();
    }, []);

    if (loading) return <p>Loading...</p>;

    return (
        <div className="my-workouts-container">
            <h2>My Workouts</h2>
            {workouts.length === 0 ? (
                <p>You haven't added any workouts yet.</p>
            ) : (
                <div className="workout-list">
                    {workouts.map(workout => (
                        <div className="workout-card" key={workout.id}>
                            <h3>{workout.name}</h3>
                            {workout.workoutExercises?.$values?.length > 0 ? (
                                <div className="exercise-list">
                                    {workout.workoutExercises.$values.map(we => (
                                        <ExerciseCard
                                            key={we.exerciseId}
                                            exercise={we.exercise}
                                            showAddButton={false}
                                        />
                                    ))}
                                </div>
                            ) : (
                                <p>No exercises in this workout yet.</p>
                            )}
                        </div>
                    ))}
                </div>
            )}
        </div>
    );
};

export default MyWorkouts;
