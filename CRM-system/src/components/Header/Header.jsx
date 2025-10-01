import Vect from '../../assets/svg/Vector.svg';
import Info from '../../assets/svg/Info.svg';
import darkMode from '../../assets/svg/DarkMode.svg';
import Exit from '../../assets/svg/Exit.svg';
import ShowMenu from '../../assets/svg/ShowMenu.svg';
import Table from '../Table/Table';
import './Header.css'
import Navigation from '../Navigation/Navigation';
import { useState } from 'react';

const manager = {
    name: 'Петров Петр Петрович',
    role: 'менеджер',
    textImg: 'GG'
}; // на будущее переделать буквы в изображении


function Header() {
    const [activeFoolMenu, setActiveFoolMenu] = useState(false);

    return (
        <>
            <div className='navigation'>
                <div className='logo'>
                    <button className='menu-button' onClick={() => setActiveFoolMenu(!activeFoolMenu)}>
                        <img src={ShowMenu} alt="" />
                    </button>
                    <img className='logo-img' src={Vect} alt="" />
                    <h1 className='logo-h1' >AUTOService</h1>
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
            <Navigation activeFoolMenu={activeFoolMenu}/>
        </>
    );
}

export default Header;

// Переделать систему для выведения инвы в профиль
// Добавь визуальное выделение активной кнопки навигации
// добавить функционал для остальных кнопок
// возможно подумать о анимации загрузки
// подтверждение выхода
// подумать над оптимизацией




