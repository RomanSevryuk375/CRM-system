import { useState } from "react"

import Header from "../components/Header/Header"
import CarCard from "../components/CarCard/CarCard"
import PPFooter from "../components/PPFooter/PPFooter"
import AddCar from "../components/AddCar/AddCar"
import ExitModal from "../components/ExitModal/ExitModal"


function PersonalPage() {
    const [addCarOpen, setAddCarOpen] = useState(false);
    const [activeExitMenu, setActiveExitMenu] = useState(false);
    return (
        <>
            <div>
                <Header 
                activeExitMenu={activeExitMenu}
                setActiveExitMenu={setActiveExitMenu}
                />
                <div className={`toolbar ${'disable'}`}>
                    <div></div>
                    <div>
                        <button 
                        onClick={() => setAddCarOpen(!addCarOpen)}
                        className='toolbar-button'>Добавить автомобиль</button>
                    </div>
                </div>
                <AddCar 
                addCarOpen={addCarOpen}
                setAddCarOpen={setAddCarOpen}
                />
                <CarCard />
                <PPFooter />
                <ExitModal 
                activeExitMenu={activeExitMenu}
                setActiveExitMenu={setActiveExitMenu}
                />
            </div>
        </>
    )
}

export default PersonalPage