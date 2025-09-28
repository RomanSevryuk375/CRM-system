import { useState } from 'react'
import Navigation from './components/Navigation/Navigation';
import Footer from './components/Footer/Footer';
import Breadcrumbs from './components/Breadcrumbs/Breadcrumbs';
import Table from './components/Table/Table';
import Toolbar from './components/Toolbar/Toolbar.jsx';
import './App.css'

function App() {
  const [count, setCount] = useState(0)
  
  return (
    <>
      <div>
        <Navigation />
        <Breadcrumbs />
        <Toolbar />
        <Table />
        <Footer /> 
      </div>
    </>
  )
}

export default App
