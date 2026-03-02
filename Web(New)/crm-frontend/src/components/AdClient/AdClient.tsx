import './AdClient.css'
import AdIMG from '../../assets/svg/AdIMG.svg'
import React from "react";

const AdClient: React.FC = () => {
    return(
        <div className='ad-content'>
            <div className='ad-text'>
                <h1 className='ad-text-h1'>РЕМОНТ С <span className='ad-pointer'>БОНУСОМ</span> НА ДОРОГУ</h1>
                <h6 className='ad-text-h6'>Получите скидку <span className='ad-pointer'>15%</span> на каршеринг!</h6>
                <span className='ad-text-span'>Пока ваша машина в заботливых руках наших мастеров, вы можете продолжать двигаться по делам выгодно и удобно с каршерингом «Михаил Колупаев». Просто укажите, что вы клиент нашего автосервиса и получите скидку 15% на все виды ремонта</span>
            </div>
            <div className='ad-img'>
                <img className='shering' src={AdIMG} alt='Shering'/>
            </div>
        </div>
    );
}
export default AdClient