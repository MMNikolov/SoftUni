window.addEventListener("load", solve);

function solve() {
    const addButtonElement = document.getElementById('add-btn')
    const nameInputElement = document.getElementById('name')
    const phoneInputElement = document.getElementById('phone')
    const categoryInputElement = document.getElementById('category')
    const checkListElement = document.getElementById('check-list')
    const contactListElement = document.getElementById('contact-list')

    addButtonElement.addEventListener('click', () => {
      const name = nameInputElement.value
      const phone = phoneInputElement.value
      const category = categoryInputElement.value

      if (!name || !phone || !category) {
        return
      }

      const namePElement = document.createElement('p')
      namePElement.textContent = `name:${name}`

      const phonePElement = document.createElement('p')
      phonePElement.textContent = `phone:${phone}`

      const categoryPElement = document.createElement('p')
      categoryPElement.textContent = `category:${category}`

      const articleElement = document.createElement('article')
      articleElement.appendChild(namePElement)
      articleElement.appendChild(phonePElement)
      articleElement.appendChild(categoryPElement)

      const editButtonElement = document.createElement('button')
      editButtonElement.className = 'edit-btn'
      
      const saveButtonElement = document.createElement('button')
      saveButtonElement.className = 'save-btn'

      const buttonsDivElement = document.createElement('div')
      buttonsDivElement.className = 'buttons'
      buttonsDivElement.appendChild(editButtonElement)
      buttonsDivElement.appendChild(saveButtonElement)

      const liElement = document.createElement('li')
      liElement.appendChild(articleElement)
      liElement.appendChild(buttonsDivElement)

      checkListElement.appendChild(liElement)

      nameInputElement.value = ''
      phoneInputElement.value = ''
      categoryInputElement.value = ''

      editButtonElement.addEventListener('click', () => {
        nameInputElement.value = name
        phoneInputElement.value = phone
        categoryInputElement.value = category

        liElement.remove()
      })
      
      saveButtonElement.addEventListener('click', () => {
        const ndNamePElement = document.createElement('p')
        ndNamePElement.textContent = `name:${name}`

        const ndPhonePElement = document.createElement('p')
        ndPhonePElement.textContent = `phone:${phone}`

        const ndCategoryPElement = document.createElement('p')
        ndCategoryPElement.textContent = `category:${category}`

        const ndArticleElement = document.createElement('article')
        ndArticleElement.appendChild(ndNamePElement)
        ndArticleElement.appendChild(ndPhonePElement)
        ndArticleElement.appendChild(ndCategoryPElement)

        const deleteButtonElement = document.createElement('button')
        deleteButtonElement.className = 'del-btn'

        const ndLiElement = document.createElement('li')
        ndLiElement.appendChild(ndArticleElement)
        ndLiElement.appendChild(deleteButtonElement)

        contactListElement.appendChild(ndLiElement)

        liElement.remove()

        deleteButtonElement.addEventListener('click', () => {
          ndLiElement.remove()
        })
      })
    })
}
  