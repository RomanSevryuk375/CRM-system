import { useState } from "react";
import Header from "../components/Header/Header";
import Footer from "../components/Footer/Footer";
import Navigation from "../components/Navigation/Navigation";
import ExitModal from "../components/ExitModal/ExitModal";
import Table from "../components/Table/Table";
import Toolbar from "../components/Toolbar/Toolbar";
import Clients from "../../src/assets/svg/Clients.svg";
import Details from "../../src/assets/svg/Details.svg";
import Home from "../../src/assets/svg/Home.svg";
import Workers from "../../src/assets/svg/Workers.svg";
import Works from "../../src/assets/svg/Works.svg";
import Task from "../../src/assets/svg/Task.svg";

const navButtonsConfig = [
  { text: "Главная", value: "main", icon: Home },
  { text: "Заказ-наряды", value: "orders", icon: Task },
  { text: "Клиенты", value: "clients", icon: Clients },
  { text: "Работники", value: "workers", icon: Workers },
  { text: "Каталог работ", value: "works", icon: Works },
  { text: "Запчасти", value: "parts", icon: Details },
  { text: "Счета", value: "bills", icon: Task }, // Временно
  { text: "Журнал оплат", value: "journal", icon: Clients }, // Временно
  { text: "Налоги", value: "taxes", icon: Workers }, // Временно
  { text: "Расходы", value: "expenses", icon: Details }, // Временно
  //
  //
  //
  //
];

function Manager() {
  const [activeFoolMenu, setActiveFoolMenu] = useState(false);
  const [activeExitMenu, setActiveExitMenu] = useState(false);
  const [activeTable, setActiveTable] = useState("main");
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
          navButtonsConfig={navButtonsConfig}
        />
        <ExitModal
          setActiveExitMenu={setActiveExitMenu}
          activeExitMenu={activeExitMenu}
        />
        <Table activeTable={activeTable} activeFoolMenu={activeFoolMenu} />
        <Toolbar
          activeTable={activeTable}
          activeFoolMenu={activeFoolMenu}
          // setPage={setPage}
        />
        <Footer />
      </div>
    </>
  );
}

export default Manager;
