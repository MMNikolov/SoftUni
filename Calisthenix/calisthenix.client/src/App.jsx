import React, { useEffect, useState } from 'react';
import { getExercises } from './services/exerciseService';
import ExerciseCard from './components/ExerciseCard/ExerciseCard';

function App() {
    const [exercises, setExercises] = useState([]);

    useEffect(() => {
        async function fetchData() {
            try {
                const data = await getExercises();
                setExercises(data);
            } catch (error) {
                console.error('Error:', error);
            }
        }
        fetchData();
    }, []);

    return (
        <div className="exercise-list">
            <h1>Calisthenics Exercises</h1>
            {exercises.map(ex => (
                <ExerciseCard key={ex.id} exercise={ex} />
            ))}
        </div>
    );
}

export default App;