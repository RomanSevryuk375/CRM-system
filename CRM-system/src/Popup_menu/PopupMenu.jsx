import ModalImg from '../assets/Cross.svg';
import './PopupMenu.css'

const ModalTitle = 'Создание заказа';
const toolBarContent = ['Основное', 'Работы', 'Запчасти', 'Итог'];
const footerContent = ['Завершить', 'Перевести в работу', 'Сохранить'];
const inputLable = ['Клиент', 'Автомобиль', 'Дата', 'Статус'];
const inputContent = [
    { id: 1, name: 'Roman', surname: 'Sevryuk' },
    { id: 2, name: 'Roman', surname: 'Sevryuk' },
    { id: 3, name: 'Roman', surname: 'Sevryuk' },
    { id: 99, name: 'Roman', surname: 'Sevryuk' },
];

function PopupMenu() {
    return (
        <div className='shadow'>
            <div className='main-menu'>
                <div className='modal-header'>
                    <h1 className='modal-title'>{ModalTitle}</h1>
                    <button className='exit'><img className='modal-img' src={ModalImg} alt="" /></button>
                </div>
                <div className='toolBar'>
                    {toolBarContent.map((item) => (<button key={item.id} className='modal-toolbar-button'>{item.name}</button>))}
                </div>
                <div className='modal-body'>
                    {inputLable.map((label) => (
                        <div key={label} className='modal-select'>
                            <p className='select-lable'>{label}</p>
                            <select className='custom-select'>
                                <option value="">Выберите {label.toLowerCase()}</option>
                                {inputContent.map((item) => (<option key={item.id} value={item.id}>{item.name} {item.surname}</option>))}
                            </select>
                        </div>
                    ))}
                </div>
                <div className='footer-button'>
                    {footerContent.map((footer) => (<button key={footer} className='custom-button'>{footer}</button>))}
                </div>
            </div>
        </div>
    );
}

export default PopupMenu;