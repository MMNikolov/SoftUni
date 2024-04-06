function addItem() {
    const itemsElement = document.getElementById('items')
    const inputElement = document.getElementById('newItemText')

    const newItemElement = document.createElement('li')
    newItemElement.textContent = inputElement.value

    const deleteElement = document.createElement('a')
    deleteElement.textContent = '[Delete]'
    deleteElement.href = '#'
    newItemElement.appendChild(deleteElement)
    itemsElement.appendChild(newItemElement)

    deleteElement.addEventListener('click', () => {
        newItemElement.remove()
    });

    inputElement.value = ''
}