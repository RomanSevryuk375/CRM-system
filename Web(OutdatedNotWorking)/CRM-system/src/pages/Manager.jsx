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
import Bills from "../../src/assets/svg/Bills.svg";
import Journal from "../../src/assets/svg/Journal.svg";
import Tax from "../../src/assets/svg/Nalogi.svg";
import ChangeMenu from "../components/Change_menu/ChangeMenu";

const navButtonsConfig = [
  { text: "Главная", value: "main", icon: Home },
  { text: "Заказ-наряды", value: "orders", icon: Task },
  { text: "Клиенты", value: "clients", icon: Clients },
  { text: "Работники", value: "workers", icon: Workers },
  { text: "Предложения", value: "propossals", icon: Journal },
  { text: "Каталог работ", value: "workTypes", icon: Journal },
  { text: "Спечиализации", value: "specializations", icon: Journal },
  { text: "Работы", value: "works", icon: Works },
  { text: "Запчасти", value: "parts", icon: Details },
  { text: "Счета", value: "bills", icon: Bills },
  { text: "Журнал оплат", value: "journal", icon: Journal },
  { text: "Налоги", value: "taxes", icon: Tax },
  { text: "Расходы", value: "expenses", icon: Journal }, 
  { text: "Поставщики", value: "suppliers", icon: Journal },
];

function Manager() {
  const [activeFoolMenu, setActiveFoolMenu] = useState(false);
  const [activeExitMenu, setActiveExitMenu] = useState(false);
  const [activeDetailing, setActiveDetailing] = useState(null);
  const [forChange, setForChange] =  useState(null);
  const [page, setPage] = useState(1);
  const [activeTable, setActiveTable] = useState("main");
  return (
    <>
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
      <div
        className={`main-content-area ${
          activeFoolMenu ? "sidebar-enabled" : "sidebar-disabled"
        }`}
      >
        <Table 
          activeTable={activeTable} 
          activeFoolMenu={activeFoolMenu} 
          activeDetailing={activeDetailing}
          setActiveDetailing={setActiveDetailing}
          forChange={forChange}
          setForChange={setForChange}
          page={page}
          setPage={setPage}
        />
      </div>
      <div
        className={`view-toolbar ${
          activeTable === "main" ? "disable" : "enable"
        }`}
      >
        <Toolbar
          activeTable={activeTable}
          activeFoolMenu={activeFoolMenu}
          setPage={setPage}
        />
      </div>
      <Footer />
      <ChangeMenu 
        isOpen={forChange !== null}
        onClose={() => setForChange(null)}
        activeTable={activeTable}
        IdForChange={forChange}
        setPage={setPage}
      />
    </>
  );
}

export default Manager;
