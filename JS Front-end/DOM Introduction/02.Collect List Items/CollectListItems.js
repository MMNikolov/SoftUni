function extractText() {
    const itemElements = document.getElementById('items')
    const textAreaElement = document.getElementById('result')

    textAreaElement.value = itemElements.textContent
}