import React from 'react';
import './Home.css';

const Home = () => {
    return (
        <div className="home">
            <div className="home-content">
                <div className="home-text">
                    <h1>Welcome to <span>Calisthenix</span>!</h1>
                    <p>Your personal guide to mastering bodyweight fitness.</p>
                    <a href="/exercises" className="home-button">Explore Exercises</a>
                    <a href="/add-workout" className="home-button">Add Exercise</a>
                    <a href="/my-workouts" className="home-button">My Workouts</a>
                </div>
                <div className="home-image">
                    <img
                        src="../src/assets/undraw_athletes-training_koqa.svg"
                        alt="Workout illustration"
                    />
                </div>
            </div>
        </div>
    );
};

export default Home;