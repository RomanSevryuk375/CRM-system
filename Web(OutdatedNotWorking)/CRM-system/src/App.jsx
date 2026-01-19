import { Routes, Route, BrowserRouter } from 'react-router-dom';
import './App.css'
import Manager from './pages/Manager';
import Client from './pages/Client';
import PersonalPage from './pages/PersonalPage';
import WorkerPage from './pages/WorkerPage';
import { Provider } from 'react-redux';
import { store } from './redux/store';

function App() {
  return (
    <>
      <Provider store={store}>
        <Routes>
          <Route path='/' element={<Client />} />
          <Route path='/manager-page' element={<Manager />} />
          <Route path='/personal-page' element={<PersonalPage />} />
          <Route path='/worker-page' element={<WorkerPage />} />
        </Routes>
      </Provider>
    </>
  )
}

export default App
