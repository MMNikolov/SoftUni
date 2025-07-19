import React, { useEffect, useState } from 'react';
import { getUsername } from '../../utils/auth';
import { toast } from 'react-toastify';
import './Profile.css';

const Profile = () => {
    const username = getUsername();
    const [avatar, setAvatar] = useState(null);
    const [showPasswordForm, setShowPasswordForm] = useState(false);
    const [passwordData, setPasswordData] = useState({
        current: '',
        new: '',
        confirm: ''
    });
    const [passwordError, setPasswordError] = useState('');

    useEffect(() => {
        const saved = localStorage.getItem('profileAvatar');
        if (saved) {
            setAvatar(saved);
        }
    }, []);

    const handleAvatarChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onloadend = () => {
                setAvatar(reader.result);
                localStorage.setItem('profileAvatar', reader.result);
            };
            reader.readAsDataURL(file);
        }
    };

    const handlePasswordChange = async (e) => {
        e.preventDefault();

        const { current, new: newPass, confirm } = passwordData;

        if (!current || !newPass || !confirm) {
            setPasswordError("All fields are required.");
            return;
        }

        if (newPass.length < 6) {
            setPasswordError("New password must be at least 6 characters.");
            return;
        }

        if (newPass !== confirm) {
            setPasswordError("New passwords do not match.");
            return;
        }

        try {
            const token = localStorage.getItem("token");
            const res = await fetch("https://localhost:7161/api/auth/change-password", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({
                    currentPassword: current,
                    newPassword: newPass,
                }),
            });

            if (!res.ok) {
                const error = await res.text();
                setPasswordError(error);
                return;
            }

            toast.success("Password changed successfully!", {
                icon: "🔐",
                style: {
                    borderRadius: '8px',
                    background: '#0066cc',
                    color: '#fff',
                    fontWeight: 'bold',
                    fontFamily: 'Segoe UI, sans-serif',
                    boxShadow: '0 4px 10px rgba(0, 0, 0, 0.15)',
                }
            });
            setPasswordData({ current: '', new: '', confirm: '' });
            setShowPasswordForm(false);
            setPasswordError('');
        } catch (err) {
            console.error(err);
            setPasswordError("Something went wrong.");
        }
    };

    return (
        <div className="profile-container">
            <div className="profile-card">
                <h2>Welcome, {username}!</h2>
                
                <div className="avatar-wrapper">
                    {avatar ? (
                        <img src={avatar} alt="Avatar" className="avatar-preview" />
                    ) : (
                        <div className="avatar-placeholder">No Avatar</div>
                    )}
                    <div className="avatar-buttons">
                        <label htmlFor="avatar-upload" className="custom-file-button">
                            Choose Image
                        </label>
                        <input
                            id="avatar-upload"
                            type="file"
                            accept="image/*"
                            onChange={handleAvatarChange}
                            style={{ display: 'none' }}
                        />

                        {avatar && (
                            <button className="remove-avatar-button" onClick={() => {
                                setAvatar(null);
                                localStorage.removeItem('profileAvatar');
                            }}>
                                Remove Avatar
                            </button>
                        )}
                    </div>
                </div>

                <div className="profile-section">
                    <strong>Username:</strong>
                    <p>{username}</p>
                </div>

                <div className="profile-actions">
                    <button onClick={() => setShowPasswordForm(!showPasswordForm)}>
                        {showPasswordForm ? 'Cancel' : 'Change Password'}
                    </button>
                    {showPasswordForm && (
                        <form className="change-password-form" onSubmit={ handlePasswordChange }>
                            <input
                                type="password"
                                placeholder="Current password"
                                value={passwordData.current}
                                onChange={(e) => setPasswordData({ ...passwordData, current: e.target.value })}
                            />
                            <input
                                type="password"
                                placeholder="New password"
                                value={passwordData.new}
                                onChange={(e) => setPasswordData({ ...passwordData, new: e.target.value })}
                            />
                            <input
                                type="password"
                                placeholder="Confirm new password"
                                value={passwordData.confirm}
                                onChange={(e) => setPasswordData({ ...passwordData, confirm: e.target.value })}
                            />
                            {passwordError && <p className="error">{passwordError}</p>}
                            <button type="submit">Save Password</button>
                        </form>
                    )}
                    <button disabled>Delete Account</button>
                </div>
            </div>
        </div>
    );
};

export default Profile;
