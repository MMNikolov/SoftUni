import React, { useEffect, useState } from 'react';
import ExerciseCard from '../ExerciseCard/ExerciseCard';
import './AllExercises.css';

function AllExercises() {
    const [exercises, setExercises] = useState([]);
    const [workouts, setWorkouts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState('');
    const [categoryFilter, setCategoryFilter] = useState('');
    const [difficultyFilter, setDifficultyFilter] = useState('');
    const [highlightId, setHighlightId] = useState(null);

    const handleDelete = (id) => {
        setExercises(prev => prev.filter(e => e.id !== id));
    };

    const filteredExercises = exercises.filter(ex => {
        return (
            ex.name.toLowerCase().includes(search.toLowerCase()) &&
            (categoryFilter === '' || ex.category === categoryFilter) &&
            (difficultyFilter === '' || ex.difficulty === difficultyFilter)
        );
    });

    useEffect(() => {
        const fetchData = async () => {
            const token = localStorage.getItem('token');
            const headers = { 'Authorization': `Bearer ${token}` };

            try {
                const [exRes, workoutRes] = await Promise.all([
                    fetch('https://localhost:7161/api/exercise', { headers }),
                    fetch('https://localhost:7161/api/workout/my', { headers })
                ]);

                if (!exRes.ok || !workoutRes.ok) {
                    const errorText = !exRes.ok ? await exRes.text() : await workoutRes.text();
                    console.error('API Error:', errorText);
                    throw new Error('Failed to fetch exercises or workouts.');
                }

                const exData = await exRes.json();
                const workoutData = await workoutRes.json();

                const allExercises = exData.$values || exData;
                const workoutList = workoutData.$values || workoutData;

                setWorkouts(workoutList);

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

        const storedId = localStorage.getItem('highlightExerciseId');
        if (storedId) {
            setHighlightId(parseInt(storedId));
            localStorage.removeItem('highlightExerciseId');
        }
    }, []);

    const handleAddToWorkout = (exerciseId) => {
        setExercises(prev => prev.filter(e => e.id !== exerciseId));
    };

    if (loading) return <p>Loading exercises...</p>;

    return (
        <div className="home-container">
            <h1>All Calisthenics Exercises</h1>
            <div className="filters">
                <input
                    type="text"
                    placeholder="Search by name..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />

                <select value={categoryFilter} onChange={(e) => setCategoryFilter(e.target.value)}>
                    <option value="">All Categories</option>
                    <option value="Push">Push</option>
                    <option value="Pull">Pull</option>
                    <option value="Legs">Legs</option>
                    <option value="Core">Core</option>
                </select>

                <select value={difficultyFilter} onChange={(e) => setDifficultyFilter(e.target.value)}>
                    <option value="">All Difficulties</option>
                    <option value="Beginner">Beginner</option>
                    <option value="Intermediate">Intermediate</option>
                    <option value="Advanced">Advanced</option>
                    <option value="Expert">Expert</option>
                </select>
            </div>
            <div className="exercise-list">
                {filteredExercises.length === 0 ? (
                    <p>No exercises match your search or filters.</p>
                ) : (
                    filteredExercises.map((exercise) => (
                        <ExerciseCard
                            key={exercise.id}
                            exercise={exercise}
                            onAdd={handleAddToWorkout}
                            onDelete={handleDelete}
                            workouts={workouts}
                            highlight={highlightId === exercise.id}
                        />
                    ))
                )}
            </div>
        </div>
    );
}

export default AllExercises;