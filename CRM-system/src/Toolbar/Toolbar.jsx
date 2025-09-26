import { useState } from 'react';
import Filtre from '../assets/Filter.svg';
import PopupMenu from '../Popup_menu/PopupMenu';
import './Toolbar.css'


function Toolbar() {
    const [isModalOpen, setIsModalOpen] = useState(false);
    return (
        <>
            <div className='toolbar'>
                <div>
                    <input id='1' className='toolbar-input' type="text" />
                    <button className='toolbar-button-filtre'><img src={Filtre} alt="1" className='toolbar-button-filtre-img' /></button>
                </div>
                <div>
                    <button className='toolbar-button'>Создать отчет</button>
                    <button onClick={() => setIsModalOpen(true)} className='toolbar-button'>Создать заказ</button>
                </div>
            </div>
            <PopupMenu isOpen={isModalOpen}
                onClose={() => setIsModalOpen(false)} />
        </>
    );
}

export default Toolbar;