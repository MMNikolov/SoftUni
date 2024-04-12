window.addEventListener("load", solve);

function solve() {
    const nameElement = document.getElementById('student')
    const universityElement = document.getElementById('university')
    const scoreElement = document.getElementById('score')
    const previewListElement = document.getElementById('preview-list')
    const nextButtonElement = document.getElementById('next-btn')
    const candidatesListElement = document.getElementById('candidates-list')

    nextButtonElement.addEventListener('click', () => {
        const name = nameElement.value
        const university = universityElement.value
        const score = scoreElement.value

        if (!name || !university || !score) {
            return;
        }

        const h4NameElement = document.createElement('h4')
        h4NameElement.textContent = name
        
        const pUniversityElement = document.createElement('p')
        pUniversityElement.textContent = `University: ${university}`
        
        const pScoreElement = document.createElement('p')
        pScoreElement.textContent = `Score: ${score}`
        
        const articleElement = document.createElement('article')
        articleElement.appendChild(h4NameElement)
        articleElement.appendChild(pUniversityElement)
        articleElement.appendChild(pScoreElement)
        
        const editButtonElement = document.createElement('button')
        editButtonElement.classList.add('action-btn', 'edit')
        editButtonElement.textContent = 'edit'
        
        const applyButtonElement = document.createElement('button')
        applyButtonElement.classList.add('action-btn', 'apply')
        applyButtonElement.textContent = 'apply'

        const applicationDivElement = document.createElement('li')
        applicationDivElement.className = 'application'
        applicationDivElement.appendChild(articleElement)
        applicationDivElement.appendChild(editButtonElement)
        applicationDivElement.appendChild(applyButtonElement)

        previewListElement.appendChild(applicationDivElement)

        nextButtonElement.setAttribute('disabled', 'disabled')

        nameElement.value = ''
        universityElement.value = ''
        scoreElement.value = ''

        editButtonElement.addEventListener('click', () => {
            nameElement.value = name
            universityElement.value = university
            scoreElement.value = score

            applicationDivElement.remove()

            nextButtonElement.removeAttribute('disabled')
        })

        applyButtonElement.addEventListener('click', () => {
            
            const ndH4NameElement = document.createElement('h4')
            ndH4NameElement.textContent = name
            
            const ndPUniversityElement = document.createElement('p')
            ndPUniversityElement.textContent = `University: ${university}`
            
            const ndPScoreElement = document.createElement('p')
            ndPScoreElement.textContent = `Score: ${score}`
            
            const ndArticleElement = document.createElement('article')
            ndArticleElement.appendChild(ndH4NameElement)
            ndArticleElement.appendChild(ndPUniversityElement)
            ndArticleElement.appendChild(ndPScoreElement)
            
            const applicationLiElement = document.createElement('li')
            applicationLiElement.className = 'application'
            applicationLiElement.appendChild(ndArticleElement)

            candidatesListElement.appendChild(applicationLiElement)

            applicationDivElement.remove()

            nextButtonElement.removeAttribute('disabled')
        })
    })
}
  