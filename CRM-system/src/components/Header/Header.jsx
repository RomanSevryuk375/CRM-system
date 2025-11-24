import Vect from '../../assets/svg/Vector.svg';
import Info from '../../assets/svg/Info.svg';
import darkMode from '../../assets/svg/DarkMode.svg';
import ShowMenu from '../../assets/svg/ShowMenu.svg';
import './Header.css'
import Account from '../Account/Account';

function Header({ activeFoolMenu, setActiveFoolMenu, activeExitMenu, setActiveExitMenu }) {
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
                    <Account />
                </div>
            </div>
        </>
    );
}

export default Header;





