import React from 'react';
import './LogoutPage.css';

const LogoutModal = ({ onConfirm, onCancel }) => {
    return (
        <div className="modal-overlay">
            <div className="modal-content slide-in">
                <h3>Are you sure you want to log out?</h3>
                <div className="modal-buttons">
                    <button className="confirm-button" onClick={onConfirm}>Yes</button>
                    <button className="cancel-button" onClick={onCancel}>No</button>
                </div>
            </div>
        </div>
    );
};

export default LogoutModal;