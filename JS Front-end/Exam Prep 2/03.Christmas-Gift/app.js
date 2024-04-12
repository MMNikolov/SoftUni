const baseURL = 'http://localhost:3030/jsonstore/gifts'

const loadPresentsButton = document.getElementById('load-presents')
const addPresentButton = document.getElementById('add-present')
const editPresentButton = document.getElementById('edit-present')
const giftListElement = document.getElementById('gift-list')
const giftInputElement = document.getElementById('gift')
const forInputElement = document.getElementById('for')
const priceInputElement = document.getElementById('price')
const formContainerElement = document.getElementById('form')

loadPresentsButton.addEventListener('click', loadPresents)

addPresentButton.addEventListener('click', () => {
    const present = getInputData()
    
    fetch(baseURL, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(present)
    })
        .then(res => {
            if(!res.ok){
                return
            }

            clearInputFields()

            return loadPresents()
        })
})

editPresentButton.addEventListener('click', editGift)

async function loadPresents(){
    const response = await fetch(baseURL)
    const presentResults = await response.json()

    giftListElement.innerHTML = ''

    const giftListFragment = document.createDocumentFragment()

    Object
        .values(presentResults)
        .forEach(present => {
            giftListFragment.appendChild(createGiftSockElement(present))
        })

    giftListElement.appendChild(giftListFragment)
}

function createGiftSockElement(present) {
    const changeButtonElement = document.createElement('button')
    changeButtonElement.classList.add('change-btn')
    changeButtonElement.textContent = 'Change'
    changeButtonElement.addEventListener('click', (e) => changeGift(e, present))

    const deleteButtonElement = document.createElement('button')
    deleteButtonElement.classList.add('delete-btn')
    deleteButtonElement.textContent = 'Delete'
    deleteButtonElement.addEventListener('click', (e) => {
        const id = present._id

        fetch(`${baseURL}/${id}`, {
            method: 'DELETE'
        })
            .then(res => {
                if (!res.ok) {
                    return
                }

                giftSockElement.remove()
            })
    })

    const divButtonContainerElement = document.createElement('div')
    divButtonContainerElement.className = 'buttons-container'
    divButtonContainerElement.appendChild(changeButtonElement)
    divButtonContainerElement.appendChild(deleteButtonElement)

    const pItemElement = document.createElement('p')
    pItemElement.textContent = present.gift

    const pPersonElement = document.createElement('p')
    pPersonElement.textContent = present.for

    const pPriceElement = document.createElement('p')
    pPriceElement.textContent = present.price

    const divContentElement = document.createElement('div')
    divContentElement.className = 'content'
    divContentElement.appendChild(pItemElement)
    divContentElement.appendChild(pPersonElement)
    divContentElement.appendChild(pPriceElement)

    const giftSockElement = document.createElement('div')
    giftSockElement.className = 'gift-sock'
    giftSockElement.appendChild(divContentElement)
    giftSockElement.appendChild(divButtonContainerElement)

    return giftSockElement
}

function changeGift(e, present) {
    const giftElement = e.currentTarget.parentElement.parentElement
    giftElement.remove()

    giftInputElement.value = present.gift
    forInputElement.value = present.for
    priceInputElement.value = present.price

    formContainerElement.setAttribute('data-id', present._id)

    editPresentButton.removeAttribute('disabled')

    addPresentButton.setAttribute('disabled', 'disabled')
}

function editGift() {
    const present = getInputData()

    const giftId = formContainerElement.getAttribute('data-id')

    formContainerElement.removeAttribute('data-id')

    fetch(`${baseURL}/${giftId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({...present, _id: giftId}),
    })
        .then(res => {
            if (!res.ok) {
                return
            }

            loadPresents()

            editPresentButton.setAttribute('disabled', 'disabled')

            addPresentButton.removeAttribute('disabled')

            clearInputFields()
        })
}

function clearInputFields() {
    giftInputElement.value = ''
    forInputElement.value = ''
    priceInputElement.value = ''
}

function getInputData() {
    const gift = giftInputElement.value
    const giftFor = forInputElement.value
    const price = priceInputElement.value

    return {
        gift,
        for: giftFor,
        price
    }
}

