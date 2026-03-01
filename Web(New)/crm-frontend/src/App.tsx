import './App.css'
import {Route, Routes} from "react-router-dom";
import Client from './Pages/Client'

function App() {
   return (
    <>
        <Routes>
            <Route path='/' element={<Client />} />
            {/*<Route path='/manager-page' element={<Manager />} />*/}
            {/*<Route path='/personal-page' element={<PersonalPage />} />*/}
            {/*<Route path='/worker-page' element={<WorkerPage />} />*/}
        </Routes>
    </>
  )
}

export default App
