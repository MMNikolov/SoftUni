window.addEventListener("load", solve);

function solve(){
    const addBtnElement = document.getElementById('add-btn');
    const expenseElement = document.getElementById('expense');
    const amountElement = document.getElementById('amount');
    const dateElement = document.getElementById('date');
    const previewListElement = document.getElementById('preview-list')
    const expenseListElement = document.getElementById('expenses-list')
    const deleteButtonElement = document.querySelector('.btn.delete')

    addBtnElement.addEventListener('click', () => {
        const expense = expenseElement.value;
        const amount = amountElement.value;
        const date = dateElement.value;

        if (!expense || !amount || !date) {
            return;
        }

        const expenseLiElement = createArticleElement(expense, amount, date)
        previewListElement.appendChild(expenseLiElement)

        addBtnElement.setAttribute('disabled', 'disable')

        expenseElement.value = ''
        amountElement.value = ''
        dateElement.value = ''

        const editButtonElement = expenseLiElement.querySelector('.btn.edit')
        const okButtonElement = expenseLiElement.querySelector('.btn.ok')

        editButtonElement.addEventListener('click', () => {
            expenseElement.value = expense
            amountElement.value = amount
            dateElement.value = date

            expenseLiElement.remove()

            addBtnElement.removeAttribute('disabled')
        })

        okButtonElement.addEventListener('click', () => {
            const expenseButtonElement = expenseLiElement.querySelector('.buttons')
            expenseButtonElement.remove()

            expenseListElement.appendChild(expenseLiElement)

            addBtnElement.removeAttribute('disabled')
        })
    })

    deleteButtonElement.addEventListener('click', () => {
        expenseListElement.innerHTML = ''
    })

    function createArticleElement(expense, amount, date){
        const pTypeElement = document.createElement('p')
        pTypeElement.textContent = `Type: ${expense}`

        const pAmountElement = document.createElement('p')
        pAmountElement.textContent = `Amount: ${amount}$`

        const pDateElement = document.createElement('p')
        pDateElement.textContent = `Date: ${date}`

        const articleElement = document.createElement('article')
        articleElement.appendChild(pTypeElement)
        articleElement.appendChild(pAmountElement)
        articleElement.appendChild(pDateElement)

        const editButtonElement = document.createElement('button')
        editButtonElement.classList.add('btn', 'edit')
        editButtonElement.textContent = 'edit'

        const okButtonElement = document.createElement('button')
        okButtonElement.classList.add('btn', 'ok')
        okButtonElement.textContent = 'ok'

        const buttonsDivElement = document.createElement('div')
        buttonsDivElement.classList.add('buttons')
        buttonsDivElement.appendChild(editButtonElement)
        buttonsDivElement.appendChild(okButtonElement)

        const liExpenseElement = document.createElement('li')
        liExpenseElement.classList.add('expense-item')
        liExpenseElement.appendChild(articleElement)
        liExpenseElement.appendChild(buttonsDivElement)

        return liExpenseElement
    }
}