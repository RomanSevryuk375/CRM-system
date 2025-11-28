import { Routes, Route, BrowserRouter } from 'react-router-dom';
import './App.css'
import Manager from './pages/Manager';
import Client from './pages/Client';
import PersonalPage from './pages/PersonalPage';
import { Provider } from 'react-redux';
import { store } from './redux/store';

function App() {
  return (
    <>
      <Provider store={store}>
        <Routes>
          <Route path='/manager' element={<Manager />} />
          <Route path='/' element={<Client />} />
          <Route path='/personal-page' element={<PersonalPage />} />
        </Routes>
      </Provider>
    </>
  )
}

export default App
