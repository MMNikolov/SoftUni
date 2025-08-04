import React, { useEffect, useState } from 'react';
import { motion } from 'framer-motion';
import { useMemo } from 'react';
import ExerciseCard from '../ExerciseCard/ExerciseCard';
import ShimmerCard from '../ShimmerCard/ShimmerCard';
import './AllExercises.css';
motion
function AllExercises() {
    const [exercises, setExercises] = useState([]);
    const [workouts, setWorkouts] = useState([]);
    const [loading, setLoading] = useState(true);
    const [search, setSearch] = useState('');
    const [categoryFilter, setCategoryFilter] = useState('');
    const [difficultyFilter, setDifficultyFilter] = useState('');
    const [toastMessage, setToastMessage] = useState(null);
    const [highlightId, setHighlightId] = useState(null);

    const handleDelete = (id, name) => {
        setExercises(prev => prev.filter(e => e.id !== id));
        setToastMessage(`"${name}" removed`);
        setTimeout(() => setToastMessage(null), 3000);
    };

    const filteredExercises = useMemo(() => {
        return exercises.filter(ex =>
            ex.name.toLowerCase().includes(search.toLowerCase()) &&
            (categoryFilter === '' || ex.category === categoryFilter) &&
            (difficultyFilter === '' || ex.difficulty === difficultyFilter)
        );
    }, [exercises, search, categoryFilter, difficultyFilter]);

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

    if (loading) {
        return (
            <div className="exercise-list">
                {Array(10).fill().map((_, i) => (
                    <ShimmerCard key={i} />
                ))}
            </div>
        );
    }

    return (
        <motion.div
            className="home-container"
            initial={{ opacity: 0, y: 20 }}
            animate={{ opacity: 1, y: 0 }}
            exit={{ opacity: 0, y: -20 }}
            transition={{ duration: 0.4 }}
        >
            <h1>All Calisthenics Exercises</h1>
            <div className="filters">
                <input
                    type="text"
                    placeholder="Search by name..."
                    value={search}
                    onChange={(e) => setSearch(e.target.value)}
                />
                <div className="filter-row">
                    <label className="filter-label">Category:</label>
                    <div className="chip-group">
                        {['', 'Push', 'Pull', 'Legs', 'Core'].map((cat) => (
                            <button
                                key={cat}
                                className={`chip ${categoryFilter === cat ? 'active' : ''}`}
                                onClick={() => setCategoryFilter(cat)}
                            >
                                {cat === '' ? 'All' : cat}
                            </button>
                        ))}
                    </div>
                </div>
                <div className="filter-row">
                    <label className="filter-label">Difficulty:</label>
                    <div className="chip-group">
                        {['', 'Beginner', 'Intermediate', 'Advanced', 'Expert'].map((lvl) => (
                            <button
                                key={lvl}
                                className={`chip ${difficultyFilter === lvl ? 'active' : ''}`}
                                onClick={() => setDifficultyFilter(lvl)}
                            >
                                {lvl === '' ? 'All Levels' : lvl}
                            </button>
                        ))}
                    </div>
                </div>
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
                            onDelete={(id) => handleDelete(id, exercise.name)}
                            workouts={workouts}
                            highlight={highlightId === exercise.id}
                        />
                    ))
                )}
            </div>
            {toastMessage && (
                <div className="toast">{toastMessage}</div>
            )}
        </motion.div>
    );
}

export default AllExercises;