window.addEventListener("load", solve);

function solve(){
    const placeInputElement = document.getElementById('place')
    const actionInputElement = document.getElementById('action')
    const personInputElement = document.getElementById('person')
    const addButtonInputElement = document.getElementById('add-btn')
    const taskListElement = document.getElementById('task-list')
    const doneListElement = document.getElementById('done-list')

    addButtonInputElement.addEventListener('click', () =>{
        const place = placeInputElement.value
        const action = actionInputElement.value
        const person = personInputElement.value
        
        if (!place || !action || !person) {
            return;
        }

        const liElement = document.createElement('li')
        liElement.className = 'clean-task'
        
        const pPLaceElement = document.createElement('p')
        pPLaceElement.textContent = `Place:${place}`
        
        const pActionElement = document.createElement('p')
        pActionElement.textContent = `Action:${action}`
        
        const pPersonElement = document.createElement('p')
        pPersonElement.textContent = `Person:${person}`

        const articleElement = document.createElement('article')
        articleElement.appendChild(pPLaceElement)
        articleElement.appendChild(pActionElement)
        articleElement.appendChild(pPersonElement)

        const editButtonElement = document.createElement('button')
        editButtonElement.className = 'edit'
        editButtonElement.textContent = 'Edit'
        
        const doneButtonElement = document.createElement('button')
        doneButtonElement.className = 'done'
        doneButtonElement.textContent = 'Done'
        
        const divElement = document.createElement('div')
        divElement.className = 'buttons'
        divElement.appendChild(editButtonElement)
        divElement.appendChild(doneButtonElement)

        liElement.appendChild(articleElement)
        liElement.appendChild(divElement)
        
        taskListElement.appendChild(liElement)

        placeInputElement.value = ''
        actionInputElement.value = ''
        personInputElement.value = ''

        editButtonElement.addEventListener('click', () => {
            placeInputElement.value = place
            actionInputElement.value = action
            personInputElement.value = person

            liElement.remove()
        })

        doneButtonElement.addEventListener('click', () => {
            const ndLiElement = document.createElement('li')

            const ndPPlaceElement = document.createElement('p')
            ndPPlaceElement.textContent = `Place:${place}`
            
            const ndPActionElement = document.createElement('p')
            ndPActionElement.textContent = `Action:${action}`
            
            const ndPPersonElement = document.createElement('p')
            ndPPersonElement.textContent = `Person:${person}`
            
            const ndArticleElement = document.createElement('article')
            ndArticleElement.appendChild(ndPPlaceElement)
            ndArticleElement.appendChild(ndPActionElement)
            ndArticleElement.appendChild(ndPPersonElement)

            const deleteButton = document.createElement('button')
            deleteButton.textContent = 'Delete'
            deleteButton.className = 'delete'

            ndLiElement.appendChild(ndArticleElement)
            ndLiElement.appendChild(deleteButton)

            doneListElement.appendChild(ndLiElement)

            liElement.remove()

            deleteButton.addEventListener('click', () => {
                ndLiElement.remove()
            })
        })
    })
}