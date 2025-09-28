import { useState } from 'react';
import Filtre from '../../assets/svg/Filter.svg';
import PopupMenu from "../../components/Popup_menu/PopupMenu.jsx";
import './Toolbar.css'


function Toolbar() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    return (
        <>
            <div className='toolbar'>
                <div>
                    <input id='toolbar-search' className='toolbar-input' type="text" />
                    <button className='toolbar-button-filtre'><img src={Filtre} alt="Фильтры" className='toolbar-button-filtre-img' /></button>
                </div>
                <div>
                    <button type="button" className='toolbar-button'>Создать отчет</button>
                    <button type="button" onClick={() => setIsModalOpen(true)} className='toolbar-button'>Создать заказ</button>
                </div>
            </div>
            <PopupMenu
                isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)}
            />
        </>
    );
}

export default Toolbar;