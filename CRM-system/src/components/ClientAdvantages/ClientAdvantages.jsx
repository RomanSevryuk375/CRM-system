import './ClientAdvantages.css'
import AdvantagesData from '../../Data/Advantages.json'

import Experience from '../../assets/svg/Experience.svg'
import Tools from '../../assets/svg/Tools.svg'
import Price from '../../assets/svg/Price.svg'
import Guarantee from '../../assets/svg/Guarantee.svg'
import Time from '../../assets/svg/Time.svg'
import Status from '../../assets/svg/Status.svg'

const iconMap = {
    "Experience": Experience,
    "Tools": Tools,
    "Price": Price,
    "Guarantee": Guarantee,
    "Time": Time,
    "Status": Status
};

function ClientAdvantages() {
    return (
        <div className='client-content-adv'>
            <h1 className='advdntages-label'>ПРЕИМУЩЕСТВА</h1>
            <div className='advdntages-content-row'>
                {AdvantagesData.map((item) => (<div className='advantage-card'>
                    <div className='advantage-card-content' key={item.id}>
                        <img className='advantage-card-img' src={iconMap[item.image]} />
                        <div className='advantage-card-text'>
                            <h1 className='advantage-card-h1'>{item.advname}</h1>
                            <label className='advantage-card-label'>{item.desc}</label>
                        </div>
                    </div>
                </div>
                ))}
            </div>
        </div>
    );
}

export default ClientAdvantages