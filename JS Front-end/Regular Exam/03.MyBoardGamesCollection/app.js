const baseURL = 'http://localhost:3030/jsonstore/games'

const loadBoardgamesButtonElement = document.getElementById('load-games')
const gamesListElement = document.getElementById('games-list')
const editGameButton = document.getElementById('edit-game')
const addButtonElement = document.getElementById('add-game')
const nameInputElement = document.getElementById('g-name')
const typeInputElement = document.getElementById('type')
const playersInputElement = document.getElementById('players')

let currentBoardGame

const loadBoardgames = async () => {
    const response = await fetch(baseURL)
    const data = await response.json()
    
    gamesListElement.innerHTML = ''

    for (const boardGame of Object.values(data)) {
        const namePElement = document.createElement('p')
        namePElement.textContent = boardGame.name

        const playersPElement = document.createElement('p')
        playersPElement.textContent = boardGame.players

        const typePElement = document.createElement('p')
        typePElement.textContent = boardGame.type

        const contentDivElement = document.createElement('div')
        contentDivElement.className = 'content'
        contentDivElement.appendChild(namePElement)
        contentDivElement.appendChild(playersPElement)
        contentDivElement.appendChild(typePElement)

        const changeButtonElement = document.createElement('button')
        changeButtonElement.className = 'change-btn'
        changeButtonElement.textContent = 'Change'

        const deleteButtonElement = document.createElement('button')
        deleteButtonElement.className = 'delete-btn'
        deleteButtonElement.textContent = 'Delete'

        const buttonsContainerDivElement = document.createElement('div')
        buttonsContainerDivElement.appendChild(changeButtonElement)
        buttonsContainerDivElement.appendChild(deleteButtonElement)

        const boardGameDivElement = document.createElement('div')
        boardGameDivElement.className = 'board-game'
        boardGameDivElement.appendChild(contentDivElement)
        boardGameDivElement.appendChild(buttonsContainerDivElement)

        gamesListElement.appendChild(boardGameDivElement)

        editGameButton.setAttribute('disabled', 'disabled')

        changeButtonElement.addEventListener('click', () => {
            currentBoardGame = boardGame._id

            nameInputElement.value = boardGame.name
            typeInputElement.value = boardGame.type
            playersInputElement.value = boardGame.players

            editGameButton.removeAttribute('disabled')

            addButtonElement.setAttribute('disabled', 'disabled')

            boardGameDivElement.remove()
        })

        deleteButtonElement.addEventListener('click', async () => {
            await fetch(`${baseURL}/${boardGame._id}`, {
                method: 'DELETE'
            })

            boardGameDivElement.remove()

            loadHistory()
        })
    }
}

loadBoardgamesButtonElement.addEventListener('click', loadBoardgames)

addButtonElement.addEventListener('click', async () => {
    const name = nameInputElement.value
    const type = typeInputElement.value
    const players = playersInputElement.value

    const response = await fetch(baseURL, {
        method: 'POST',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            name,
            type,
            players,
        })
    })

    if (!response.ok) {
        return;
    }

    nameInputElement.value = ''
    typeInputElement.value = ''
    playersInputElement.value = ''

    await loadBoardgames()
})

editGameButton.addEventListener('click', async () => {
    const name = nameInputElement.value
    const type = typeInputElement.value
    const players = playersInputElement.value

    const response = await fetch(`${baseURL}/${currentBoardGame}`, {
        method: 'PUT',
        headers: {
            'content-type': 'application/json'
        },
        body: JSON.stringify({
            _id: currentBoardGame,
            name,
            type,
            players
        })
    })

    if (!response.ok) {
        return
    }

    await loadBoardgames()

    editGameButton.setAttribute('disabled', 'disabled')
    
    addButtonElement.removeAttribute('disabled')

    currentHistory = null

    nameInputElement.value = ''
    typeInputElement.value = ''
    playersInputElement.value = ''
})

