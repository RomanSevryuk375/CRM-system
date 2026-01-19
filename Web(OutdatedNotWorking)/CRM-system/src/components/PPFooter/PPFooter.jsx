import './PPFooter.css'
import Facebook from '../../assets/svg/Facebook.svg' 
import Inst from '../../assets/svg/Inst.svg'
import Vk from '../../assets/svg/Vk.svg'
import Youtube from '../../assets/svg/Youtube.svg'

function PPFooter() {
    return (
        <footer className='ppfooter'>
            <div>
                <h1 className='logo-h1-ppfooter' >AUTOService</h1>
            </div>
            <div className='inc-container'> 
                <span className='inc'>© 2025 AutoService</span>
            </div>
            <div className='ppfooter-button-container'>
                <button className='footer-button-pp'>
                    <img className='ppfooter-img' src={Facebook} />
                </button>
                <button className='footer-button-pp'>
                    <img className='ppfooter-img' src={Inst} />
                </button>
                <button className='footer-button-pp'>
                    <img className='ppfooter-img' src={Vk} />
                </button>
                <button className='footer-button-pp'>
                    <img className='ppfooter-img' src={Youtube} />
                </button>
            </div>
        </footer>
    );
}

export default PPFooter;