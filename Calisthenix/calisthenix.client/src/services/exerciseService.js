const API = '/api/exercise';

export async function getExercises() {
    const res = await fetch(API);
    if (!res.ok) throw new Error('Failed to load');
    return res.json();
}

export async function getExercise(id) {
    const res = await fetch(`${API}/${id}`);
    if (!res.ok) throw new Error('Not found');
    return res.json();
}

export async function addExercise(ex) {
    const res = await fetch(API, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(ex)
    });
    if (!res.ok) throw new Error('Add failed');
    return res.json();
}

export async function updateExercise(id, ex) {
    const res = await fetch(`${API}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(ex)
    });
    if (!res.ok) throw new Error('Update failed');
    return res.json();
}

export async function deleteExercise(id) {
    const res = await fetch(`${API}/${id}`, { method: 'DELETE' });
    if (!res.ok) throw new Error('Delete failed');
}