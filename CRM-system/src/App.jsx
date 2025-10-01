import { useState } from 'react'
import Header from './components/Header/Header.jsx';
import Footer from './components/Footer/Footer';
import Breadcrumbs from './components/Breadcrumbs/Breadcrumbs';

import './App.css'

function App() {  
  return (
    <>
      <div>
        <Header />
        {/* <Breadcrumbs /> */}
        <Footer /> 
      </div>
    </>
  )
}

export default App
