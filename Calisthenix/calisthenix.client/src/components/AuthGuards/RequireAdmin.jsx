import { Navigate } from 'react-router-dom';
import { jwtDecode } from 'jwt-decode';

function RequireAdmin({ children }) {
    const token = localStorage.getItem('token');

    if (!token) return <Navigate to="/login" />;

    try {
        const decoded = jwtDecode(token);
        const role = decoded.role;

        if (role === 'Admin') {
            return children;
        }

        return <Navigate to="/not-authorized" />;
    } catch {
        return <Navigate to="/login" />;
    }
}

export default RequireAdmin;
