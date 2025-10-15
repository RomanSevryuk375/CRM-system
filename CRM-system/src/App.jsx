import { Routes, Route } from 'react-router-dom';
import './App.css'
import Manager from './pages/Manager';
import Client from './pages/Client';
import PersonalPage from './pages/PersonalPage';



function App() {  
  return (
    <>
      <Routes>
          <Route path='/' element={<Manager />}/>
          <Route path='/client' element={<Client />}/>
          <Route path='/personal-page' element={<PersonalPage />}/>
      </Routes>
    </>
  )
}

export default App
