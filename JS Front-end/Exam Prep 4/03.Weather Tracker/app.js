const baseURL = 'http://localhost:3030/jsonstore/tasks'

const loadHistoryButtonElement = document.getElementById('load-history')
const historyListElement = document.getElementById('list')
const editButtonElement = document.getElementById('edit-weather')
const addButtonElement = document.getElementById('add-weather')
const locationInputElement = document.getElementById('location')
const temperatureInputElement = document.getElementById('temperature')
const dateInputElement = document.getElementById('date')

let currentHistory

const loadHistory = async () => {
    const response = await fetch(baseURL)
    const data = await response.json()
    
    historyListElement.innerHTML = ''

    for (const history of Object.values(data)) {
        const containerDivElement = document.createElement('div')
        containerDivElement.className = 'container'

        const locationH2Element = document.createElement('h2')
        locationH2Element.textContent = history.location

        const dateH3Element = document.createElement('h3')
        dateH3Element.textContent = history.date

        const temperatureH3Element = document.createElement('h3')
        temperatureH3Element.textContent = history.temperature
        temperatureH3Element.setAttribute('id', 'celsius')

        const buttonsContainerDivElement = document.createElement('div')
        buttonsContainerDivElement.className = 'buttons-container'

        const changeButtonElement = document.createElement('button')
        changeButtonElement.className = 'change-btn'
        changeButtonElement.textContent = 'Change'

        const deleteButtonElement = document.createElement('button')
        deleteButtonElement.className = 'delete-btn'
        deleteButtonElement.textContent = 'Delete'

        buttonsContainerDivElement.appendChild(changeButtonElement)
        buttonsContainerDivElement.appendChild(deleteButtonElement)

        containerDivElement.appendChild(locationH2Element)
        containerDivElement.appendChild(dateH3Element)
        containerDivElement.appendChild(temperatureH3Element)
        containerDivElement.appendChild(buttonsContainerDivElement)

        historyListElement.appendChild(containerDivElement)

        editButtonElement.setAttribute('disabled', 'disabled')

        changeButtonElement.addEventListener('click', () => {
            currentHistory = history._id
            
            locationInputElement.value = history.location
            temperatureInputElement.value = history.temperature
            dateInputElement.value = history.date

            editButtonElement.removeAttribute('disabled')

            addButtonElement.setAttribute('disabled', 'disabled')

            containerDivElement.remove()
        })

        deleteButtonElement.addEventListener('click', async () => {
            await fetch(`${baseURL}/${history._id}`, {
                method: 'DELETE'
            })

            containerDivElement.remove()

            loadHistory()
        })
    }
}

loadHistoryButtonElement.addEventListener('click', loadHistory)

addButtonElement.addEventListener('click', async () => {
    const location = locationInputElement.value
    const temperature = temperatureInputElement.value
    const date = dateInputElement.value

    const response = await fetch(baseURL, {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            location,
            temperature,
            date,
        })
    })

    
    if (!response.ok) {
        return;
    }
    
    
    locationInputElement.value = ''
    temperatureInputElement.value = ''
    dateInputElement.value = ''
    
    await loadHistory()
})

editButtonElement.addEventListener('click', async () => {
    const location = locationInputElement.value
    const temperature = temperatureInputElement.value
    const date = dateInputElement.value

    const response = await fetch(`${baseURL}/${currentHistory}`, {
        method: 'PUT',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            _id: currentHistory,
            location,
            temperature,
            date
        })
    })

    if (!response.ok) {
        return
    }

    editButtonElement.setAttribute('disabled', 'disabled')
    
    addButtonElement.removeAttribute('disabled')
    
    currentHistory = null

    locationInputElement.value = ''
    temperatureInputElement.value = ''
    dateInputElement.value = ''

    await loadHistory()
})