import './CarCard.css'
import CarCardData from '../../Data/CarCards.json'

import Car from '../../assets/svg/Car.svg'
import Vin from '../../assets/svg/Vin.svg'
import StateNumber from '../../assets/svg/GosNumber.svg'
import Milage from '../../assets/svg/Probeg.svg'
import StatusMin from '../../assets/svg/StatusMin.svg'

function CarCard() {
    return (
        <div className='car-card-screen'>
            {CarCardData.map((item) => (
                <div
                key={item.car_id}
                className='car-card-content'>
                    <div className='car-buttons'>
                        <h1 className='car-buttons-model'>{item.car_name}</h1>
                        <img className='car-buttons-img' src={Car} />
                        <div className='car-buttons-button-container'>
                            <button className='car-buttons-button'>История визитов</button>
                            <button className='car-buttons-delete'>Удалить</button>
                        </div>
                    </div>
                    <div className='car-info'>
                        <h1 className='car-info-model'>Технические характеристики</h1>
                        <div className='car-info-cards'>
                            <div className='card-info-card'>
                                <img className='card-info-img' src={Vin} />
                                <h2 className='card-info-label'>VIN-номер</h2>
                                <span className='card-info-span'>{item.car_vin}</span>
                            </div>
                            <div className='card-info-card'>
                                <img className='card-info-img' src={StateNumber} />
                                <h2 className='card-info-label'>Гос. номер</h2>
                                <span className='card-info-span'>{item.car_state_number}</span>
                            </div>
                            <div className='card-info-card'>
                                <img className='card-info-img' src={Milage} />
                                <h2 className='card-info-label'>Пробег</h2>
                                <span className='card-info-span'>{item.car_milage}</span>
                            </div>
                            <div className='card-info-card'>
                                <img className='card-info-img' src={StatusMin} />
                                <h2 className='card-info-label'>Статус  заказа</h2>
                                <span className='card-info-span'>{item.prder_status}</span>
                            </div>
                        </div>
                    </div>
                </div>
            ))}
            {/* <button className='add-auto'>
                Добавить автомобиль
            </button> */}
        </div>

    )
}

export default CarCard