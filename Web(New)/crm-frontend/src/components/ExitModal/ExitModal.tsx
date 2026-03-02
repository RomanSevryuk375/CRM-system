import { useNavigate } from 'react-router-dom';
import './ExitModal.css'
import Cross from '../../assets/svg/Cross.svg';
import * as React from "react";
import type {ExitModalProps} from "../../types/ExitModalProps.ts";
import {useAuthStore} from "../../store/useAuthStore.ts";

const ExitModal:React.FC<ExitModalProps> = ({ activeExitMenu, setActiveExitMenu })=> {
    const navigate = useNavigate();

    const logout = useAuthStore((state) => state.logout);

    if (!activeExitMenu) {
        return null;
    }

    const handleClose = () => {
        setActiveExitMenu(false);
    };

    const handleLogout = async () => {
        logout();
        setActiveExitMenu(false);
        navigate('/');
    };

    return (
        <div className='shadow' onClick={handleClose}>
            <div className='content-ExitMenu'>
                <div className='block-ExitMenu-header'>
                    <label className='ExitMenu-lable'>Выход из системы</label>
                    <button className='block-ExitMenu-header-button'>
                        <img
                            src={Cross}
                            className='ExitMenu-img'
                            onClick={() => (setActiveExitMenu(!activeExitMenu))}
                            alt='Exit'
                        />
                    </button>
                </div>
                <div className='block-ExitMenu-body'>
                    <span className='block-ExitMenu-body-text'>Вы уверены, что хотите выйти из системы?</span>
                </div>
                <div className='block-ExitMenu-footer'>
                    <button
                        className='block-ExitMenu-footer-button'
                        onClick={handleClose}>Отмена</button>
                    <button
                        className='block-ExitMenu-footer-button'
                        onClick={handleLogout}>Ок
                    </button>
                </div>
            </div>
        </div>
    );
}

export default ExitModal;