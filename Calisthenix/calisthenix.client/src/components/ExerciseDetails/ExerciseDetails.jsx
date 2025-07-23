import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import './ExerciseDetails.css';

const ExerciseDetails = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [exercise, setExercise] = useState(null);
    const [comments, setComments] = useState([]);
    const [newComment, setNewComment] = useState('');

    useEffect(() => {
        fetch(`https://localhost:7161/api/exercise/${id}`)
            .then(res => res.json())
            .then(data => setExercise(data))
            .catch(err => console.error('Error fetching exercise:', err));

        fetch(`https://localhost:7161/api/comments/${id}`)
            .then(res => res.json())
            .then(data => {
                const safeComments = Array.isArray(data)
                    ? data
                    : data?.$values || []; 
                setComments(safeComments);
            })
            .catch(err => {
                console.error('Error fetching comments:', err);
                setComments([]); 
            });
    }, [id]);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const token = localStorage.getItem('token');

        try {
            const res = await fetch(`https://localhost:7161/api/comments/${id}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${token}`
                },
                body: JSON.stringify(newComment)
            });

            if (res.ok) {
                setComments(prev => [
                    {
                        username: 'You',
                        content: newComment,
                        createdAt: new Date().toISOString()
                    },
                    ...prev
                ]);
                setNewComment('');
            } else {
                console.error('Failed to post comment.');
            }
        } catch (err) {
            console.error('Error posting comment:', err);
        }
    };

    if (!exercise) return <div>Loading...</div>;

    return (
        <div className="exercise-details-wrapper">
            <div className="exercise-details-left">
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

            <div className="exercise-details-right comment-section">
                <h2>Comments</h2>
                <form onSubmit={handleSubmit} className="comment-form">
                    <textarea
                        value={newComment}
                        onChange={(e) => setNewComment(e.target.value)}
                        rows="3"
                        placeholder="Write a comment..."
                        required
                    />
                    <button type="submit">Post</button>
                </form>

                {comments.length === 0 ? (
                    <p>No comments yet. Be the first!</p>
                ) : (
                    comments.map((c, index) => (
                        <div className="comment" key={index}>
                            <div className="comment-header">
                                <span>{c.username || "Anonymous"}</span>
                                <span>{new Date(c.createdAt).toLocaleString()}</span>
                            </div>
                            <p>{c.content}</p>
                        </div>
                    ))
                )}
            </div>
        </div>
    );
};

export default ExerciseDetails;