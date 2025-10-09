import ModalImg from '../../assets/svg/Cross.svg';
import { useState } from 'react';
import './PopupMenu.css'

const ModalTitle = 'Создание заказа';
const toolBarContent = [
    { name: 'Основное', value: 'basic' },
    { name: 'Работы', value: 'works' },
    { name: 'Запчасти', value: 'parts' },
    { name: 'Итог', value: 'result' }
];
const footerContent = ['Завершить', 'Перевести в работу', 'Сохранить'];

const inputLableBasic = ['Клиент','Автомобиль','Дата','Статус']
const inputLableWorks = ['Работы', 'Мастер'];
const inputLableParts = ['Поставщик', 'Наименование', 'Артикул', 'Количество', 'Цена за еденицу (BYN)'];
const inputLableResult = ['Стоимость работ (BYN)', 'Стоимость запчастей (BYN)', 'Итоговая стоимость (BYN)', 'Статус'];

// const inputContentBasic = [
//     { id: 1, name: 'Roman', surname: 'Sevryuk' },
//     { id: 2, name: 'Roman', surname: 'Sevryuk' },
//     { id: 3, name: 'Roman', surname: 'Sevryuk' },
//     { id: 99, name: 'Roman', surname: 'Sevryuk' }
// ];

const GenericPopupMenu = ({ ModalTitle, toolBarContent, footerContent, inputLable, inputContent, onClose, setCategoryMenu, isOpen }) => {
    if (!isOpen) {
        return null;
    }
    return (
        <div className='shadow'>
            <div className='main-menu'>
                <div className='modal-header'>
                    <h1 className='modal-title'>
                        {ModalTitle}
                    </h1>
                    <button
                        onClick={onClose}
                        className='exit'>
                        <img className='modal-img' src={ModalImg} alt="" />
                    </button>
                </div>
                <div className='toolBar'>
                    {toolBarContent.map((row, index) => (
                        <button
                            onClick={() => setCategoryMenu(row.value)}
                            key={index}
                            className='modal-toolbar-button'>
                            {row.name}
                        </button>
                    ))}
                </div>
                <div className='modal-body'>
                    {inputLable.map((label) => (
                        <div
                            key={label}
                            className='modal-select'>
                            <p className='select-lable'>
                                {label}
                            </p>
                            <select className='custom-select'>
                                <option value="">
                                    Выберите {label.toLowerCase()}
                                </option>
                                {/* {inputContent.map((item) => (
                                    <option
                                        key={item.id}
                                        value={item.id}>
                                        {item.name} {item.surname}
                                    </option>
                                ))} */}
                            </select>
                        </div>
                    ))}
                </div>
                <div className='footer-button'>
                    {footerContent.map((footer) => (
                        <button
                            key={footer}
                            className='custom-button'>
                            {footer}
                        </button>
                    ))}
                </div>
            </div>
        </div>
    );
}

function PopupMenu({ isOpen, onClose }) {
    const [categoryMenu, setCategoryMenu] = useState('basic');
    switch (categoryMenu) {
        case 'basic':
            return (
                <GenericPopupMenu
                    ModalTitle={ModalTitle}
                    toolBarContent={toolBarContent}
                    footerContent={footerContent}
                    inputLable={inputLableBasic}
                    // inputContent={inputContentBasic}
                    onClose={onClose}
                    isOpen={isOpen}
                    setCategoryMenu={setCategoryMenu}
                />
            )
            case 'works':
             return (
                <GenericPopupMenu
                    ModalTitle={ModalTitle}
                    toolBarContent={toolBarContent}
                    footerContent={footerContent}
                    inputLable={inputLableWorks}
                    // inputContent={inputContentBasic}
                    onClose={onClose}
                    isOpen={isOpen}
                    setCategoryMenu={setCategoryMenu}
                />
            )  
            case 'parts':
             return (
                <GenericPopupMenu
                    ModalTitle={ModalTitle}
                    toolBarContent={toolBarContent}
                    footerContent={footerContent}
                    inputLable={inputLableParts}
                    // inputContent={inputContentBasic}
                    onClose={onClose}
                    isOpen={isOpen}
                    setCategoryMenu={setCategoryMenu}
                />
            ) 
            case 'result':
             return (
                <GenericPopupMenu
                    ModalTitle={ModalTitle}
                    toolBarContent={toolBarContent}
                    footerContent={footerContent}
                    inputLable={inputLableResult}
                    // inputContent={inputContentBasic}
                    onClose={onClose}
                    isOpen={isOpen}
                    setCategoryMenu={setCategoryMenu}
                />
            )   
    }


}

export default PopupMenu;

// поправить разметку
// поправить селекты
// прописать поля селектов массивом полей
// ошибки валидации данных