import { useState } from 'react'
import Navigation from './Navigation/Navigation';
import Vect from './assets/Vector.svg';
import './App.css'

function App() {
  const [count, setCount] = useState(0)

  return (
    <>
      <div><Navigation /></div>
    </>
  )
}

export default App
