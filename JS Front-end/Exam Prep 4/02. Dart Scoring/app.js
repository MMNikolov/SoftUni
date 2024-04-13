window.addEventListener("load", solve);

function solve() {
  const addButtonElement = document.getElementById('add-btn')
  const playerInputElement = document.getElementById('player')
  const scoreInputElement = document.getElementById('score')
  const roundInputElement = document.getElementById('round')
  const sureListElement = document.getElementById('sure-list')
  const scoreboardListElement = document.getElementById('scoreboard-list')
  const buttonClearElement = document.querySelector('.clear')

  addButtonElement.addEventListener('click', () => {
    const player = playerInputElement.value
    const score = scoreInputElement.value
    const round = roundInputElement.value

    if (!player || !score || !round) {
      return
    }

    const pPlayerElement = document.createElement('p')
    pPlayerElement.textContent = player
    
    const pScoreElement = document.createElement('p')
    pScoreElement.textContent = `Score: ${score}`
    
    const pRoundElement = document.createElement('p')
    pRoundElement.textContent = `Round: ${round}`
    
    const articleElement = document.createElement('article')
    articleElement.appendChild(pPlayerElement)
    articleElement.appendChild(pScoreElement)
    articleElement.appendChild(pRoundElement)
    
    const editButtonElement = document.createElement('button')
    editButtonElement.className = 'btn edit'
    editButtonElement.textContent = 'edit'
    
    const okButtonElement = document.createElement('button')
    okButtonElement.className = 'btn ok'
    okButtonElement.textContent = 'ok'
    
    const dartItemLiElement = document.createElement('li')
    dartItemLiElement.className = 'dart-item'
    dartItemLiElement.appendChild(articleElement)
    dartItemLiElement.appendChild(editButtonElement)
    dartItemLiElement.appendChild(okButtonElement)
    
    sureListElement.appendChild(dartItemLiElement)
    
    addButtonElement.setAttribute('disabled', 'disabled')
    
    playerInputElement.value = ''
    scoreInputElement.value = ''
    roundInputElement.value = ''

    editButtonElement.addEventListener('click', () => {
      playerInputElement.value = player
      scoreInputElement.value = score
      roundInputElement.value = round
    
      dartItemLiElement.remove()
    
      addButtonElement.removeAttribute('disabled')
    })

    okButtonElement.addEventListener('click', () => {
      
      const ndPPlayerElement = document.createElement('p')
      ndPPlayerElement.textContent = player
      
      const ndPScoreElement = document.createElement('p')
      ndPScoreElement.textContent = `Score: ${score}`
      
      const ndPRoundElement = document.createElement('p')
      ndPRoundElement.textContent = `Round: ${round}`
      
      const ndArticleElement = document.createElement('article')
      ndArticleElement.appendChild(ndPPlayerElement)
      ndArticleElement.appendChild(ndPScoreElement)
      ndArticleElement.appendChild(ndPRoundElement)
      
      const ndDartItemLiElement = document.createElement('li')
      ndDartItemLiElement.className = 'dart-item'
      ndDartItemLiElement.appendChild(ndArticleElement)

      scoreboardListElement.appendChild(ndDartItemLiElement)

      dartItemLiElement.remove()

      addButtonElement.removeAttribute('disabled')
    })
  })

  buttonClearElement.addEventListener('click', () => {
    location.reload()
  })
}
