import Vect from '../assets/Vector.svg';
import Info from '../assets/Info.svg';
import darkMode from '../assets/DarkMode.svg';
import Exit from '../assets/Exit.svg';
import './Navigation.css'

const Name = 'Петров Петр Петрович'
const Role = 'менеджер'
const Text = 'GG'
function Navigation() {
    return (
        <div className='navigation'>
            <div className='logo'>
                <img className='logo-img' src={Vect} alt="" />
                <h1 className='logo-h1' >AUTOService</h1>
            </div>
            <div className='buttons'>
                <button className='navigation-button'>ЗАКАЗ-НАРЯДЫ</button>
                <button className='navigation-button' >КЛИЕНТЫ</button>
                <button className='navigation-button' >МАСТЕРА</button>
                <button className='navigation-button' >РАБОТЫ</button>
                <button className='navigation-button' >ЗАПЧАСТИ</button>
            </div>
            <div className='profile'>
                <button className='profile-button'><img src={Info} alt="Information" /></button>
                <button className='profile-button'><img src={darkMode} alt="" /></button>
                <div className='profile-mini'><p className='profile-mini-text'>{Text}</p></div>
                <div className='profile-user-role'>
                    <h1 className='profile-user'>{Name}</h1>
                    <p className='profile-role'>{Role}</p>
                </div>
                <button className='profile-button'><img src={Exit} alt="" /></button>
            </div>
        </div>
    );
}

export default Navigation;
