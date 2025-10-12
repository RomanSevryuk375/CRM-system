import './ServicesClient.css'
import ServiceClient from '../../Data/ServiceClient.json'

import Diagnostics from '../../assets/svg/Diagnostics.svg'
import Change from '../../assets/svg/Change.svg'
import Suspension from '../../assets/svg/Suspension.svg'
import Electrician from '../../assets/svg/Electrician.svg'
import Tires from '../../assets/svg/Tires.svg'
import Brake from '../../assets/svg/Brake.svg'

const iconMap = {
    "Diagnostics": Diagnostics,
    "Change": Change,
    "Suspension": Suspension,
    "Electrician": Electrician,
    "Tires": Tires,
    "Brake": Brake
};

function ServicesClient() {
    return (
        <div className='client-content-ser'>
            <h1 className='service-label'>УСЛУГИ</h1>
            <div className='service-content-row'>
                {ServiceClient.map((item) => (
                    <div className='service-card' key={item.id}>
                    <div className='service-card-content'>
                        <div className='service-card-img'>
                            <img className='service-img' src={iconMap[item.ser_img]} />
                        </div>
                        <div className='service-card-text'>
                            <h1 className='service-card-h1'>{item.ser_name}</h1>
                            <label className='service-card-label'>{item.ser_desc}</label>
                        </div>
                    </div>
                </div>
                ))}
            </div>
        </div>
    );
}

export default ServicesClient