const baseURL = 'http://localhost:3030/jsonstore/tasks'

const loadVacationsButtonElement = document.getElementById('load-vacations')
const vacationListElement = document.getElementById('list')
const nameInputElement = document.getElementById('name')
const daysInputElement = document.getElementById('num-days')
const dateInputElement = document.getElementById('from-date')
const addButtonElement = document.getElementById('add-vacation')
const editButtonElement = document.getElementById('edit-vacation')

let currentVacation 

const loadVacations = async () => {
    const response = await fetch(baseURL)
    const data = await response.json()
    
    vacationListElement.innerHTML = ''
    
    for (const vacation of Object.values(data)) {
        const containerDivElement = document.createElement('div')
        containerDivElement.className = 'container'
        
        const nameH2Element = document.createElement('h2')
        nameH2Element.textContent = vacation.name
        
        const dateH3Element = document.createElement('h3')
        dateH3Element.textContent = vacation.date
        
        const daysH3Element = document.createElement('h3')
        daysH3Element.textContent = vacation.days
        
        const changeButtonElement = document.createElement('button')
        changeButtonElement.className = 'change-btn'
        changeButtonElement.textContent = 'Change'
        
        const doneButtonElement = document.createElement('button')
        doneButtonElement.className = 'done-btn'
        doneButtonElement.textContent = 'Done'
        
        containerDivElement.appendChild(nameH2Element)
        containerDivElement.appendChild(dateH3Element)
        containerDivElement.appendChild(daysH3Element)
        containerDivElement.appendChild(changeButtonElement)
        containerDivElement.appendChild(doneButtonElement)
        
        vacationListElement.appendChild(containerDivElement)

        changeButtonElement.addEventListener('click', () => {
            currentVacation = vacation._id

            nameInputElement.value = vacation.name
            daysInputElement.value = vacation.days
            dateInputElement.value = vacation.date

            editButtonElement.removeAttribute('disabled')

            addButtonElement.setAttribute('disabled', 'disabled')

            containerDivElement.remove()
        })

        doneButtonElement.addEventListener('click', async () => {
            await fetch(`${baseURL}/${vacation._id}`, {
                method: 'DELETE'
            })

            containerDivElement.remove()
        })
    }
}

loadVacationsButtonElement.addEventListener('click', loadVacations);

addButtonElement.addEventListener('click', async () => {
    const name = nameInputElement.value
    const days = daysInputElement.value
    const date = dateInputElement.value

    const response = await fetch(baseURL, {
      method: 'POST',
       headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            name,
            days,
            date,
        })
    })

    if (!response.ok) {
        return;
    }

    await loadVacations()

    nameInputElement.value = ''
    daysInputElement.value = ''
    dateInputElement.value = ''
    
})

editButtonElement.addEventListener('click', async () => {
    const name = nameInputElement.value
    const days = daysInputElement.value
    const date = dateInputElement.value

    const response = await fetch(`${baseURL}/${currentVacation}`, {
        method: 'PUT',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            _id: currentVacation,
            name,
            days,
            date
        })
    })

    if (!response.ok) {
        return
    }

    editButtonElement.setAttribute('disabled', 'disabled')
    
    addButtonElement.removeAttribute('disabled')
    
    currentMeal = null

    nameInputElement.value = ''
    daysInputElement.value = ''
    dateInputElement.value = ''

    await loadVacations()
})

