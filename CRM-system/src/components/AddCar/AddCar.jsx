import './Addcar.css'
import { useEffect } from 'react'
import Cross from '../../assets/svg/Cross.svg'

const input = ['Название','Модель','Год','VIN номер','Государственный номер','Пробег'];


function AddCar({ addCarOpen, setAddCarOpen }) {
    if (!addCarOpen) {
        return null
    }
    useEffect(() => {
        const handleKeyPress = (event) => {
            if (event.key === 'Escape') {
                setAddCarOpen(!addCarOpen)
            }
        };

        document.addEventListener('keydown', handleKeyPress);
    }, []);
    return (
        <div className="shadow">
            <div className="main-menu">
                <div className='modal-header'>
                    <h1 className='modal-title'>
                        Добавить авто
                    </h1>
                    <button
                        onClick={() => setAddCarOpen(!addCarOpen)}
                        className='exit'>
                        <img className='modal-img' src={Cross} alt="" />
                    </button>
                </div>
                <div className='modal-body'>
                    {input.map((item) => (
                        <div
                        key={item}
                        className='modal-select'>
                        <p className='select-lable'>{item}</p>
                        <input className='custom-input-auto' placeholder={'Введите ' + item.toLowerCase()} type="text" />
                    </div>
                    ))}
                </div>
                <div className='footer-button'>
                        <button 
                        onClick={() => setAddCarOpen(!addCarOpen)}
                        className='custom-button'>
                            Отмена
                        </button>
                        <button 
                        onClick={() => setAddCarOpen(!addCarOpen)}
                        className='custom-button'>
                            Добавить
                        </button>
                </div>
            </div>
        </div>
    )

}

export default AddCar