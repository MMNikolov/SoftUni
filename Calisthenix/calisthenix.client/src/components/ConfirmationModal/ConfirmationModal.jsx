import React from 'react';
import './ConfirmationModal.css';

const ConfirmationModal = ({ title, message, onConfirm, onCancel }) => {
    return (
        <div className="modal-overlay">
            <div className="modal-content slide-in">
                <h3>{title}</h3>
                <p>{message}</p>
                <div className="modal-buttons">
                    <button className="confirm-button" onClick={onConfirm}>Yes</button>
                    <button className="cancel-button" onClick={onCancel}>No</button>
                </div>
            </div>
        </div>
    );
};

export default ConfirmationModal;
