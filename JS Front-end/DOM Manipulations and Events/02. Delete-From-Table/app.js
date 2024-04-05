function deleteByEmail() {
    const inputEmailElement = document.querySelector('input[name = email]')
    const resultElement = document.getElementById('result')
    const tableRowElements = document.querySelectorAll('table#customers tbody tr')

    const trElement = Array
        .from(tableRowElements)
        .find(element => element.children[1].textContent.includes(inputEmailElement.value))

    if (trElement) {
        trElement.remove()
        resultElement.textContent = 'Deleted'
    } else{
        resultElement.textContent = 'Not found.'
    }
}