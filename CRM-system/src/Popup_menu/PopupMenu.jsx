import ModalImg from '../assets/Cross.svg';
import './PopupMenu.css'

const ModalTitle = 'Создание заказа';
const toolBarContent = ['Основное', 'Работы', 'Запчасти', 'Итог'];
const inputContent = [

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
                    {toolBarContent.map((item) => (<button key={item} className='modal-toolbar-button'>{item}</button>))}
                </div>
                <div className='modal-body'>
                    <div className='modal-select'>
                        <p className='select-lable'>Клиент</p>
                        <select className='custom-select'>
                            <option value="client">Выберите клиента</option>
                        </select>
                    </div>
                    <div className='modal-select'>
                        <p className='select-lable'>Автомобиль</p>
                        <select className='custom-select'>
                            <option value="car">Выберите автомобиль</option>
                        </select>
                    </div>
                    <div className='modal-select'>
                        <p className='select-lable'>Дата</p>
                        <select className='custom-select'>
                            <option value="date">Выберите дату</option>
                        </select>
                    </div>
                    <div className='modal-select'>
                        <p className='select-lable'>Статус</p>
                        <select className='custom-select'>
                            <option value="status">Выберите статус</option>
                        </select>
                    </div>
                </div>
                <div className='footer-button'>
                    <button className='custom-button'>Сохранить</button>
                    <button className='custom-button'>Перевести вработу</button>
                    <button className='custom-button'>Сохранить</button>
                </div>
            </div>
        </div>
    );
}

export default PopupMenu;