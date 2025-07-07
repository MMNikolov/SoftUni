import React, { useEffect, useState } from 'react';
import ExerciseCard from '../ExerciseCard/ExerciseCard';
import ConfirmationModal from '../ConfirmationModal/ConfirmationModal';
import './MyWorkouts.css';

const MyWorkouts = () => {
    const [workouts, setWorkouts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [showModal, setShowModal] = useState(false);
    const [pendingDelete, setPendingDelete] = useState({ workoutId: null, exerciseId: null });
    const [toastMessage, setToastMessage] = useState(null);

    const requestRemove = (workoutId, exerciseId) => {
        setPendingDelete({ workoutId, exerciseId });
        setShowModal(true);
    };

    const confirmRemove = () => {
        const { workoutId, exerciseId } = pendingDelete;
        setShowModal(false);

        handleRemove(workoutId, exerciseId);
        setToastMessage("Exercise removed successfully!");

        setTimeout(() => setToastMessage(null), 3000);
    };

    const cancelRemove = () => {
        setShowModal(false);
        setPendingDelete({ workoutId: null, exerciseId: null });
    };

    const handleRemove = async (workoutId, exerciseId) => {
        const token = localStorage.getItem('token');

        try {
            const res = await fetch(`https://localhost:7161/api/workout/${workoutId}/exercise/${exerciseId}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (!res.ok) throw new Error('Failed to remove exercise.');

            setWorkouts(prev =>
                prev.map(w => {
                    if (w.id !== workoutId) return w;

                    return {
                        ...w,
                        workoutExercises: {
                            $values: w.workoutExercises.$values.filter(e => e.exerciseId !== exerciseId)
                        }
                    };
                })
            );
        } catch (err) {
            console.error(err);
            alert('Error removing exercise.');
        }
    };

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

    if (loading) {
        return (
            <div className="spinner-container">
                <div className="spinner"></div>
                <p>Loading your workouts...</p>
            </div>
        );
    }

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
                                            onRemove={() => requestRemove(workout.id, we.exerciseId)}
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

            {showModal && (
                <ConfirmationModal
                    title="Remove Exercise"
                    message="Are you sure you want to remove this exercise from the workout?"
                    onConfirm={confirmRemove}
                    onCancel={cancelRemove}
                />
            )}
            {toastMessage && (
                <div className="toast">{toastMessage}</div>
            )}
        </div>

    );
};

export default MyWorkouts;
