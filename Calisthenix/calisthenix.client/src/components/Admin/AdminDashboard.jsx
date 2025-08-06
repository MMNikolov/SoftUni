import React, { useEffect, useState } from 'react';
import './AdminDashboard.css';

function AdminDashboard() {
    const [users, setUsers] = useState([]);
    const [error, setError] = useState('');

    const fetchUsers = async () => {
        try {
            const token = localStorage.getItem('token');
            const res = await fetch('https://localhost:7161/api/admin/users', {
                headers: { Authorization: `Bearer ${token}` }
            });

            if (res.ok) {
                const data = await res.json(); // ✅ read once only
                console.log("[Admin fetch result]:", data);
                setUsers(data.$values || []);  // ✅ use $values from EF circular serialization
            } else {
                setError("Access denied or not authorized.");
            }
        } catch (err) {
            console.error("Error fetching users:", err);
            setError("Failed to load users.");
        }
    };

    const deleteUser = async (id) => {
        const token = localStorage.getItem('token');
        const res = await fetch(`https://localhost:7161/api/admin/users/${id}`, {
            method: 'DELETE',
            headers: { Authorization: `Bearer ${token}` }
        });

        if (res.ok) {
            setUsers(prev => prev.filter(u => u.id !== id));
        } else {
            alert("Failed to delete user.");
        }
    };

    const promoteUser = async (id) => {
        const token = localStorage.getItem('token');
        const res = await fetch(`https://localhost:7161/api/admin/users/${id}/promote`, {
            method: 'PUT',
            headers: { Authorization: `Bearer ${token}` }
        });

        if (res.ok) {
            fetchUsers(); // refresh list
        } else {
            alert("Failed to promote user.");
        }
    };

    useEffect(() => {
        fetchUsers();
    }, []);

    return (
        <div className="admin-dashboard">
            <h2>Admin Dashboard</h2>
            {error && <p>{error}</p>}
            <table>
                <thead>
                    <tr>
                        <th>Username</th>
                        <th>Role</th>
                        <th>Actions</th>
                    </tr>
                </thead>
                <tbody>
                    {Array.isArray(users) && users.length > 0 ? (
                        users.map(user => (
                            <tr key={user.id}>
                                <td>{user.username}</td>
                                <td>
                                    <span className={`role-badge ${user.role === 'Admin' ? 'role-admin' : 'role-user'}`}>
                                        {user.role}
                                    </span>
                                </td>
                                <td>
                                    <button className="delete" onClick={() => deleteUser(user.id)}>Delete</button>
                                    {user.role !== "Admin" && (
                                        <button onClick={() => promoteUser(user.id)}>Promote</button>
                                    )}
                                </td>
                            </tr>
                        ))
                    ) : (
                        <tr><td colSpan="3">No users found or not authorized.</td></tr>
                    )}
                </tbody>
            </table>
        </div>
    );
}

export default AdminDashboard;
