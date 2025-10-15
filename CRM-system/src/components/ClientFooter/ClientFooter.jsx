import './ClientFooter.css'
import Facebook from '../../assets/svg/Facebook.svg' 
import Inst from '../../assets/svg/Inst.svg'
import Vk from '../../assets/svg/Vk.svg'
import Youtube from '../../assets/svg/Youtube.svg'

function ClientFooter() {
    return (
        <>
        <div className='footer-content'>
            <div>
                <h1 className='logo-h1-footer' >AUTOService</h1>
            </div>
            <div>
                <span className='inc'>© 2025 AutoService</span>
            </div>
            <div className='footer-button-container'>
                <button className='footer-button-cli'>
                    <img className='footer-img' src={Facebook} />
                </button>
                <button className='footer-button-cli'>
                    <img className='footer-img' src={Inst} />
                </button>
                <button className='footer-button-cli'> 
                    <img className='footer-img' src={Vk} />
                </button>
                <button className='footer-button-cli'>
                    <img className='footer-img' src={Youtube} />
                </button>
            </div>
        </div>
        </>
        
    )
}

export default ClientFooter