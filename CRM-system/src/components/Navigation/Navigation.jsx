import Clients from '../../assets/svg/Clients.svg';
import Details from '../../assets/svg/Details.svg';
import Home from '../../assets/svg/Home.svg';
import Workers from '../../assets/svg/Workers.svg';
import Works from '../../assets/svg/Works.svg';
import Task from '../../assets/svg/Task.svg';

import { useState } from 'react';
import Table from '../Table/Table.jsx';
import Toolbar from '../Toolbar/Toolbar.jsx';
import './Navigation.css'

const navButtons = [
    { text: 'Главная', value: 'main', icon: Home },
    { text: 'Заказ-наряды', value: 'orders', icon: Task },
    { text: 'Клиенты', value: 'clients', icon: Clients },
    { text: 'Работники', value: 'workers', icon: Workers },
    { text: 'Каталог работ', value: 'works', icon: Works },
    { text: 'Запчасти', value: 'parts', icon: Details },
    { text: 'Счета', value: 'bills', icon: Task }, // Временно
    { text: 'Журнал оплат', value: 'journal', icon: Clients }, // Временно
    { text: 'Налоги', value: 'taxes', icon: Workers }, // Временно
    { text: 'Расходы', value: 'expenses', icon: Details }, // Временно
];

function Navigation({ activeFoolMenu,setActiveTable }) {
    return (
        <>
            <div className={`buttons ${activeFoolMenu ? 'expanded' : 'folded'}`}>
                {navButtons.map((row, index) => (
                    <button
                        key={index}
                        className={`navigation-button ${activeFoolMenu ? 'expanded' : 'folded'}`}
                        onClick={() => setActiveTable(row.value)}
                    >
                        <img className={`button-img ${activeFoolMenu ? 'expanded' : 'folded'}`}
                            src={row.icon}
                            alt={row.text}
                        />
                        {activeFoolMenu && (
                            <span className='button-text'>{row.text}</span>
                        )}
                    </button>
                ))}
            </div>
        </>
    );
}

export default Navigation;