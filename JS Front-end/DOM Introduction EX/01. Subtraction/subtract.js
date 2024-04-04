function subtract() {
    const firstElement = document.getElementById('firstNumber')
    const secondElement = document.getElementById('secondNumber')
    const resultElement = document.getElementById('result')

    const firstNum = Number(firstElement.value)
    const secondNum = Number(secondElement.value)
    const result = firstNum - secondNum

    resultElement.textContent = result
}