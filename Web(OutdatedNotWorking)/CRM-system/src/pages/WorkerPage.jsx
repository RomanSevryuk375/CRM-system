import "./index.css";

import { useState } from "react";
import Task from "../assets/svg/Task.svg";
import Bills from "../assets/svg/Bills.svg";
import Journal from "../assets/svg/Journal.svg";
import Header from "../components/Header/Header";
import ExitModal from "../components/ExitModal/ExitModal";
import Navigation from "../components/Navigation/Navigation";
import Table from "../components/Table/Table";
import Toolbar from "../components/Toolbar/Toolbar";
import Works from "../assets/svg/Works.svg";
import Details from "../assets/svg/Details.svg";
import Home from "../../src/assets/svg/Home.svg";
import ChangeMenu from "../components/Change_menu/ChangeMenu";

const navButtonsConfig = [
  { text: "Автомобили", value: "carsWorker", icon: Home },
  { text: "Заказ-наряды", value: "ordersWorker", icon: Task },
  { text: "Работы", value: "worksWorker", icon: Works },
  { text: "История посещений", value: "historyWorker", icon: Journal },
  { text: "Запчасти", value: "partsWorker", icon: Details },
  { text: "Предложения", value: "proposalWorker", icon: Bills },
];

function PersonalPage() {
  const [activeFoolMenu, setActiveFoolMenu] = useState(false);
  const [activeExitMenu, setActiveExitMenu] = useState(false);
  const [activeTable, setActiveTable] = useState("carsWorker");
  const [isMod, setIsMod] = useState(false);
  const [forChange, setForChange] =  useState(null);
  const [page, setPage] = useState(1);
  return (
    <>
      <Header
        activeExitMenu={activeExitMenu}
        setActiveExitMenu={setActiveExitMenu}
        activeFoolMenu={activeFoolMenu}
        setActiveFoolMenu={setActiveFoolMenu}
      />
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
      <Navigation
        activeFoolMenu={activeFoolMenu}
        activeTable={activeTable}
        setActiveTable={setActiveTable}
        navButtonsConfig={navButtonsConfig}
      />
      <div
        className={`main-content-area ${
          activeFoolMenu ? "sidebar-enabled" : "sidebar-disabled"
        }`}
      >
        <Table
          isMod={isMod}
          setIsMod={setIsMod}
          activeTable={activeTable}
          activeFoolMenu={activeFoolMenu}
          page={page}
          setPage={setPage}
        />
      </div>
      <ExitModal
        activeExitMenu={activeExitMenu}
        setActiveExitMenu={setActiveExitMenu}
      />
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

export default PersonalPage;
