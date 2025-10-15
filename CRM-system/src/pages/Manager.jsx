import { useState } from 'react';
import Header from '../components/Header/Header';
import Footer from '../components/Footer/Footer';
import Navigation from '../components/Navigation/Navigation';
import ExitModal from '../components/ExitModal/ExitModal';
import Table from '../components/Table/Table';
import Toolbar from '../components/Toolbar/Toolbar';


function Manager() {
  const [activeFoolMenu, setActiveFoolMenu] = useState(false);
  const [activeExitMenu, setActiveExitMenu] = useState(false);
  const [activeTable, setActiveTable] = useState('main');
  return (
    <>
      <div>
        <Header
          activeFoolMenu={activeFoolMenu}
          setActiveFoolMenu={setActiveFoolMenu}
          activeExitMenu={activeExitMenu}
          setActiveExitMenu={setActiveExitMenu}
        />
        <Navigation
          activeFoolMenu={activeFoolMenu}
          activeTable={activeTable}
          setActiveTable={setActiveTable}
        />
        <ExitModal
          setActiveExitMenu={setActiveExitMenu}
          activeExitMenu={activeExitMenu}
        />
        <Table
          activeTable={activeTable}
          activeFoolMenu={activeFoolMenu}
        />
        <Toolbar
          activeFoolMenu={activeFoolMenu}
        />
        <Footer />
      </div>
    </>
  )
}

export default Manager