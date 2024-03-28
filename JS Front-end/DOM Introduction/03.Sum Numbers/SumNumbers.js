function calc() {
    const firstElement = document.getElementById('num1')
    const secondElement = document.getElementById('num2')
    const sumElement = document.getElementById('sum')

    const firstNum = Number(firstElement.value)
    const secondNum = Number(secondElement.value)
    const result = firstNum + secondNum

    sumElement.value = result
}
