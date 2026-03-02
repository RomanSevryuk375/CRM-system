import './ClientBanner.css'
import Registration from '../Registration/Registration.tsx';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import vectorCar from '../../assets/svg/Vector.svg';
import info from '../../assets/svg/Info.svg';
import darkMode from '../../assets/svg/DarkMode.svg';
import Account from '../Account/Account';
import { HashLink } from 'react-router-hash-link';
import {useAuthStore} from "../../store/useAuthStore.ts";


const ClientBanner: React.FC = () => {
    const [registrationIsOpen, setRegistrationIsOpen] = useState(false);
    const navigate = useNavigate();

    const user = useAuthStore((state) => state.user);

    const handleActionClick = () => {
        if (user && user.roleId === 2) {
            navigate('/personal-page');
        } else {
            setRegistrationIsOpen(true);
        }
    };

    return (
        <>
            <div className='client-content'>
                <div className='banner'>
                    <header className='navigation-cli'>
                        <div className='logo-cli'>
                            <img className='logo-img-cli' src={vectorCar} alt="" />
                            <h1 className='logo-h1-cli' >AUTOService</h1>
                        </div>
                        <div className='navigation-buttons-cli'>
                            <HashLink smooth to="#advantages" className="nav-link-wrapper">
                                <button className='navigation-button-cli'>Преимущества</button>
                            </HashLink>
                            <HashLink smooth to="#services" className="nav-link-wrapper">
                                <button className='navigation-button-cli'>Услуги</button>
                            </HashLink>
                            <HashLink smooth to="#faq" className="nav-link-wrapper">
                                <button className='navigation-button-cli'>Часто задаваемые вопросы</button>
                            </HashLink>
                            <HashLink smooth to="#contacts" className="nav-link-wrapper">
                                <button className='navigation-button-cli'>Контакты</button>
                            </HashLink>
                        </div>
                        <div className='profile-cli'>
                            <button className='profile-button-cli'><img src={info} alt="Information-cli" /></button>
                            <button className='profile-button-cli'><img src={darkMode} alt="" /></button>
                            <Account
                                registrationIsOpen={registrationIsOpen}
                                setRegistrationIsOpen={setRegistrationIsOpen}
                            />
                        </div>
                    </header>
                    <div className='content-blok-cli'>
                        <div className='content-blok-text-cli'>
                            <h1 className='main-text-cli'>КАЧЕСТВЕННЫЙ <span className='text-pointer'>РЕМОНТ</span> ЛЮБОГО ТРАНСПОРТНОГО СРЕДСТВА <br /><span className='text-pointer'>С ГАРАНТИЕЙ</span> НА РАБОТУ</h1>
                            <span className='main-text-desc-cli'>Гарантия на работу с двигателем до 3 лет<br /> На мелкосрочный ремонт до 6 месяцев</span>
                        </div>
                        <div className='content-block-buttons-cli'>
                            <button
                                className='banner-button-cli-active'
                                onClick={handleActionClick}
                            >Заказать диагностику</button>
                            <button
                                className='banner-button-cli'
                                onClick={handleActionClick}
                            >Консультация</button>
                        </div>
                    </div>
                </div>
            </div>
            <Registration
                registrationIsOpen={registrationIsOpen}
                setRegistrationIsOpen={setRegistrationIsOpen}
            />
        </>
    )
}

export default ClientBanner;