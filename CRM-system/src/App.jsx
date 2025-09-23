import { useState } from 'react'
import Navigation from './Navigation/Navigation';
import Footer from './Footer/Footer';
import Breadcrumbs from './Breadcrumbs/Breadcrumbs';
import Table from './Table/Table';
import Toolbar from './Toolbar/Toolbar';
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
