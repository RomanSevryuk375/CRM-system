import { Routes, Route } from 'react-router-dom';
import './App.css'
import Manager from './pages/Manager';
import Client from './pages/Client';



function App() {  
  return (
    <>
      <Routes>
          <Route path='/' element={<Manager />}/>
          <Route path='/client' element={<Client />}/>
      </Routes>
    </>
  )
}

export default App
