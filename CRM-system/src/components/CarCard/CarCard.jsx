import './CarCard.css'

import Car from '../../assets/svg/Car.svg'
import Vin from '../../assets/svg/Vin.svg'
import StateNumber from '../../assets/svg/GosNumber.svg'
import Milage from '../../assets/svg/Probeg.svg'
import StatusMin from '../../assets/svg/StatusMin.svg'
import { useDispatch, useSelector } from 'react-redux'
import { useEffect, useState } from 'react'
import { deleteCar, getMyCars } from '../../redux/Actions/cars'
import InfiniteScroll from 'react-infinite-scroll-component'

function CarCard({ isMod, setIsMod, page, setPage }) {
    const myCars = useSelector(state => state.cars.myCars);
    const myCarsTotal = useSelector(state => state.cars.myCarsTotal);
    const isLoggedIn = useSelector(state => state.users.isLoggedIn);
    const dispatch = useDispatch();

    useEffect(() => {
        if (isLoggedIn) {
            dispatch(getMyCars(page));
        }
    }, [isLoggedIn, page, isMod, dispatch]);

    const handleDeleteCar = (carId) => {

        dispatch(deleteCar(carId));
        setIsMod(!isMod);

        setPage(1);
    };

    const nextHandler = () => {
        if (myCars.length >= myCarsTotal) return;

        setPage(prev => prev + 1);
    };

    const hasMore = myCarsTotal !== null && myCars.length < myCarsTotal;
    if (!isLoggedIn) return null;
    return (
        <InfiniteScroll
            dataLength={myCars.length}
            next={nextHandler}
            hasMore={hasMore}
            scrollableTarget="container"
        >
            <div className='car-card-screen' id="container">
                {myCars.map((item) => (
                    <div
                        key={item.id}
                        className='car-card-content'>
                        <div className='car-buttons'>
                            <h1 className='car-buttons-model'>{item.brand} {item.model}</h1>
                            <img className='car-buttons-img' src={Car} />
                            <div className='car-buttons-button-container'>
                                <button className='car-buttons-button'>История визитов</button>
                                <button
                                    className='car-buttons-delete'
                                    onClick={() => handleDeleteCar(item.id)}>Удалить</button>
                            </div>
                        </div>
                        <div className='car-info'>
                            <h1 className='car-info-model'>Технические характеристики</h1>
                            <div className='car-info-cards'>
                                <div className='card-info-card'>
                                    <img className='card-info-img' src={Vin} />
                                    <h2 className='card-info-label'>VIN-номер</h2>
                                    <span className='card-info-span'>{item.vinNumber}</span>
                                </div>
                                <div className='card-info-card'>
                                    <img className='card-info-img' src={StateNumber} />
                                    <h2 className='card-info-label'>Гос. номер</h2>
                                    <span className='card-info-span'>{item.stateNumber}</span>
                                </div>
                                <div className='card-info-card'>
                                    <img className='card-info-img' src={Milage} />
                                    <h2 className='card-info-label'>Пробег</h2>
                                    <span className='card-info-span'>{item.mileage}</span>
                                </div>
                                <div className='card-info-card'>
                                    <img className='card-info-img' src={StatusMin} />
                                    <h2 className='card-info-label'>ID автомобиля</h2>
                                    <span className='card-info-span'>{item.id}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </InfiniteScroll>
    )
}

export default CarCard;