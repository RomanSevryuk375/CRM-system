import { useNavigate } from 'react-router-dom';
import './ExitModal.css'
import Cross from '../../assets/svg/Cross.svg';
import { useDispatch } from 'react-redux';
import { logoutUser } from '../../redux/Actions/users';

function ExitModal({ activeExitMenu, setActiveExitMenu }) {
    const navigate = useNavigate();
    const dispatch = useDispatch();

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
                        onClick={() => {
                            dispatch(logoutUser());
                            navigate('/');
                        }}>Ок
                    </button>
                </div>
            </div>
        </div>
    );
}

export default ExitModal;