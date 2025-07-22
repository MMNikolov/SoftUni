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
    const [newWorkoutName, setNewWorkoutName] = useState('');
    const [showCreateForm, setShowCreateForm] = useState(false);
    const [editingWorkoutId, setEditingWorkoutId] = useState(null);
    const [editingName, setEditingName] = useState('');
    const [showWorkoutDeleteModal, setShowWorkoutDeleteModal] = useState(false);
    const [workoutToDelete, setWorkoutToDelete] = useState(null);



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

    const handleCreateWorkout = async (e) => {
        e.preventDefault();
        const token = localStorage.getItem('token');

        try {
            const res = await fetch('https://localhost:7161/api/workout/create', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify({ name: newWorkoutName })
            });

            if (!res.ok) throw new Error('Failed to create workout');

            const newWorkout = await res.json();
            setWorkouts(prev => [...prev, newWorkout]);
            setToastMessage('Workout created!');
            setNewWorkoutName('');
            setShowCreateForm(false);

            setTimeout(() => setToastMessage(null), 3000);
        } catch (err) {
            console.error(err);
            alert('Error creating workout.');
        }
    };

    const handleRenameWorkout = async (id) => {
        const token = localStorage.getItem('token');

        const res = await fetch(`https://localhost:7161/api/workout/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${token}`
            },
            body: JSON.stringify({ name: editingName })
        });

        if (!res.ok) {
            console.error(await res.text());
            return;
        }

        setWorkouts(prev =>
            prev.map(w =>
                w.id === id ? { ...w, name: editingName } : w
            )
        );
        setEditingWorkoutId(null);
        setEditingName('');
        setToastMessage("Workout renamed!");

        setTimeout(() => setToastMessage(null), 3000);
    };

    const handleDeleteWorkout = async (id) => {
        const token = localStorage.getItem('token');

        try {
            const res = await fetch(`https://localhost:7161/api/workout/${id}`, {
                method: 'DELETE',
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (!res.ok) throw new Error("Failed to delete workout");

            setWorkouts(prev => prev.filter(w => w.id !== id));
            setToastMessage("Workout deleted!");
            setTimeout(() => setToastMessage(null), 3000);
        } catch (err) {
            console.error(err);
            alert("Error deleting workout.");
        }
    };

    const confirmWorkoutDelete = async () => {
        if (!workoutToDelete) return;
        await handleDeleteWorkout(workoutToDelete);
        setWorkoutToDelete(null);
        setShowWorkoutDeleteModal(false);
    };

    const cancelWorkoutDelete = () => {
        setWorkoutToDelete(null);
        setShowWorkoutDeleteModal(false);
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
            <div className="create-workout-header">
                <button className="create-workout-button" onClick={() => setShowCreateForm(!showCreateForm)}>
                    {showCreateForm ? 'Cancel' : '➕ Create Workout'}
                </button>

                {showCreateForm && (
                    <form className="create-workout-form" onSubmit={handleCreateWorkout}>
                        <input
                            type="text"
                            placeholder="Workout name..."
                            value={newWorkoutName}
                            onChange={(e) => setNewWorkoutName(e.target.value)}
                            required
                        />
                        <button type="submit">Save</button>
                    </form>
                )}
            </div>
            <h2>My Workouts</h2>
            {workouts.length === 0 ? (
                <p>You haven't added any workouts yet.</p>
            ) : (
                <div className="workout-list">
                    {workouts.map(workout => (
                        <div className="workout-card" key={workout.id}>
                            <div className="workout-title">
                                {editingWorkoutId === workout.id ? (
                                    <>
                                        <input
                                            value={editingName}
                                            onChange={(e) => setEditingName(e.target.value)}
                                            onKeyDown={(e) => {
                                                if (e.key === 'Enter') handleRenameWorkout(workout.id);
                                            }}
                                            autoFocus
                                        />
                                        <button className="save-btn" onClick={() => handleRenameWorkout(workout.id)}>
                                            💾
                                        </button>
                                    </>
                                ) : (
                                    <>
                                        <h3>{workout.name}</h3>
                                        <div className="workout-actions">
                                            <button className="icon-btn" onClick={() => {
                                                setEditingWorkoutId(workout.id);
                                                setEditingName(workout.name);
                                            }}>
                                                ✏️
                                            </button>
                                                <button className="icon-btn delete-btn" onClick={() => {
                                                    setWorkoutToDelete(workout.id);
                                                    setShowWorkoutDeleteModal(true);
                                                }}>
                                                    ❌
                                                </button>
                                        </div>
                                    </>
                                )}
                            </div>
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
            {showWorkoutDeleteModal && (
                <ConfirmationModal
                    title="Delete Workout"
                    message="Are you sure you want to delete this entire workout?"
                    onConfirm={confirmWorkoutDelete}
                    onCancel={cancelWorkoutDelete}
                />
            )}
        </div>

    );
};

export default MyWorkouts;
