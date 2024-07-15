import { useState } from 'react'
import './App.css'

function App() {
  
  const [movies, setMovies] = useState([
    'The Matrix',
    "Man of steel",
    'Lord of the rings'
  ])

  const buttonClickHandler = () => {
    setMovies((oldMovies) => {
      const newMovies = [...oldMovies]
      newMovies[2] = 'Harry Potter'

      return newMovies
    })
  }
  
  return (
    <>
      <h1>Movies</h1>
      <ul>
        {movies.map(movie => <li key={movie}>{movie}</li>)} 
      </ul>

      <button onClick={buttonClickHandler}>Change</button>
    </>
  )
}

export default App
