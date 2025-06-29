import { jwtDecode } from 'jwt-decode';

export function getUsername() {
    const token = localStorage.getItem('token');
    if (!token) return null;

    try {
        const decoded = jwtDecode(token);
        return decoded.name || decoded.unique_name || decoded.sub || null;
    } catch {
        return null;
    }
}

export function isAuthenticated() {
    const token = localStorage.getItem('token');
    if (!token) return false;

    try
    {
        const { exp } = jwtDecode(token);
        return Date.now() < exp * 1000;
    }
    catch {
        return false;
    }
}

export function isLoggedIn() {
    const token = localStorage.getItem('token');
    if (!token) return false;

    try {
        const decoded = jwtDecode(token);
        return Date.now() < decoded.exp * 1000;
    } catch {
        return false;
    }
}

export function logout() {
    localStorage.removeItem('token');
    window.location.href = '/';
}