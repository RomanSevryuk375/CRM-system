import Vect from '../../assets/svg/Vector.svg';
import Info from '../../assets/svg/Info.svg';
import darkMode from '../../assets/svg/DarkMode.svg';
import Exit from '../../assets/svg/Exit.svg';
import Table from '../Table/Table';
import './Navigation.css'
import { useState } from 'react';

const manager = {
    name: 'Петров Петр Петрович',
    role: 'менеджер',
    textImg: 'GG'
}; // на будущее переделать буквы в изображении
const navButtons = [
    { text: 'ЗАКАЗ-НАРЯДЫ', value: 'orders' },
    { text: 'КЛИЕНТЫ', value: 'clients' },
    { text: 'МАТСЕРА', value: 'workers' },
    { text: 'РАБОТЫ', value: 'works' },
    { text: 'ЗАПЧАСТИ', value: 'parts' }
]; //вроде норм сделано 

function Navigation() {
    const [activeTable, setActiveTable] = useState('orders');
    return (
        <>
            <div className='navigation'>
                <div className='logo'>
                    <img className='logo-img' src={Vect} alt="" />
                    <h1 className='logo-h1' >AUTOService</h1>
                </div>
                <div className='buttons'>
                    {navButtons.map((row, index) => (
                        <button key={index} className='navigation-button'
                            onClick={() => setActiveTable(row.value)}>
                            {row.text}
                        </button>))}
                </div>
                <div className='profile'>
                    <button className='profile-button'><img src={Info} alt="Information" /></button>
                    <button className='profile-button'><img src={darkMode} alt="" /></button>
                    <div className='profile-mini'><p className='profile-mini-text'>{manager.textImg}</p></div>
                    <div className='profile-user-role'>
                        <h1 className='profile-user'>{manager.name}</h1>
                        <p className='profile-role'>{manager.role}</p>
                    </div>
                    <button className='profile-button'><img src={Exit} alt="" /></button>
                </div>
            </div>
            <Table
                activeTable={activeTable}
            />
        </>
    );
}

export default Navigation;

// Переделать систему для выведения инвы в профиль