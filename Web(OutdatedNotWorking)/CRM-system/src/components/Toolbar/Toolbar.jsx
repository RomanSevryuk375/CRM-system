import { useState } from 'react';
import Filtre from '../../assets/svg/Filter.svg';
import PopupMenu from "../../components/Popup_menu/PopupMenu.jsx";
import './Toolbar.css'


function Toolbar({ activeFoolMenu, activeTable, setPage}) {
    const [isModalOpen, setIsModalOpen] = useState(false);
    return (
        <>
            <div className={`toolbar ${activeFoolMenu ? 'enable' : 'disable'}`}>
                <div>
                    <input id='toolbar-search' className='toolbar-input' type="text" />
                    <button className='toolbar-button-filtre'><img src={Filtre} alt="Фильтры" className='toolbar-button-filtre-img' /></button>
                </div>
                <div>
                    <button type="button" onClick={() => setIsModalOpen(true)} className='toolbar-button'>Создать</button>
                </div>
            </div>
            <PopupMenu
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
                activeTable={activeTable}
                setPage={setPage}
            />
        </>
    );
}

export default Toolbar;

// поправить стили
// сделать обработку эксейпа
// добавить обработчики событий