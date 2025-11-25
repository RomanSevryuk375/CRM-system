import { useState } from "react"
import Task from "../assets/svg/Task.svg";
import Rashodi from "../assets/svg/Rashodi.svg";
import Bills from "../assets/svg/Bills.svg";
import Home from "../assets/svg/Home.svg";
import Journal from "../assets/svg/Journal.svg";
import Header from "../components/Header/Header";
import PPFooter from "../components/PPFooter/PPFooter";
import ExitModal from "../components/ExitModal/ExitModal";
import Navigation from "../components/Navigation/Navigation";
import Table from "../components/Table/Table";
import Toolbar from "../components/Toolbar/Toolbar";

const navButtonsConfig = [
    { text: 'Главная', value: 'mainClient', icon: Home },
    { text: 'Заказ-наряды', value: 'ordersClient', icon: Bills },
    { text: 'История посещений', value: 'historyClient', icon: Task },
    { text: 'Счета', value: 'billsClient', icon: Rashodi },
    { text: 'Журнал оплат', value: 'journalClient', icon: Journal },
];

function PersonalPage() {
    const [activeFoolMenu, setActiveFoolMenu] = useState(false);
    const [activeExitMenu, setActiveExitMenu] = useState(false);
    const [activeTable, setActiveTable] = useState('mainClient');
    const [isMod, setIsMod] = useState(false);
    const [page, setPage] = useState(1);
    return (
        <>
            <div>
                <Header
                    activeExitMenu={activeExitMenu}
                    setActiveExitMenu={setActiveExitMenu}
                    activeFoolMenu={activeFoolMenu}
                    setActiveFoolMenu={setActiveFoolMenu}
                />
                <Toolbar 
                    activeTable={activeTable}
                    activeFoolMenu={activeFoolMenu}
                    setPage={setPage}
                />
                <Navigation
                    activeFoolMenu={activeFoolMenu}
                    activeTable={activeTable}
                    setActiveTable={setActiveTable}
                    navButtonsConfig={navButtonsConfig}
                />
                <Table
                    isMod={isMod}
                    setIsMod={setIsMod}
                    activeTable={activeTable}
                    activeFoolMenu={activeFoolMenu}
                />
                <ExitModal
                    activeExitMenu={activeExitMenu}
                    setActiveExitMenu={setActiveExitMenu}
                />
            </div>
        </>
    )
}

export default PersonalPage;