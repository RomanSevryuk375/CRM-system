import { useNavigate } from 'react-router-dom';
import './ExitModal.css'
import Cross from '../../assets/svg/Cross.svg';

function ExitModal({ activeExitMenu, setActiveExitMenu }) {
    const navigate = useNavigate();

    if (!activeExitMenu) {
        return null;
    }

    return (
        <div className='shadow'>
            <div className='content-ExitMenu'>
                <div className='block-ExitMenu-header'>
                    <label className='ExitMenu-lable'>Выход из системы</label>
                    <button className='block-ExitMenu-header-button'>
                        <img
                            src={Cross}
                            className='ExitMenu-img'
                            onClick={() => (setActiveExitMenu(!activeExitMenu))} />
                    </button>
                </div>
                <div className='block-ExitMenu-body'>
                    <span className='block-ExitMenu-body-text'>Вы уверены, что хотите выйти из системы?</span>
                </div>
                <div className='block-ExitMenu-footer'>
                    <button
                        className='block-ExitMenu-footer-button'
                        onClick={() => (setActiveExitMenu(!activeExitMenu))}>Отмена</button>
                    <button
                        className='block-ExitMenu-footer-button'
                        onClick={() => navigate('/Client')}>Ок
                    </button>
                </div>
            </div>
        </div>
    );
}

export default ExitModal;