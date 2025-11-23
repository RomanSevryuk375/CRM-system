import './Addcar.css'
import { useEffect, useState } from 'react'
import Cross from '../../assets/svg/Cross.svg'
import { useDispatch } from 'react-redux';
import { createCar } from '../../redux/Actions/cars';

function AddCar({ addCarOpen, setAddCarOpen }) {
    const dispatch = useDispatch();

    const [addForm, setAddForm] = useState({
        ownerId: 0,
        brand: "",
        model: "",
        yearOfManufacture: 0,
        vinNumber: "",
        stateNumber: "",
        mileage: 0
    });

    if (!addCarOpen) {
        return null
    }
    useEffect(() => {
        const handleKeyPress = (event) => {
            if (event.key === 'Escape') {
                setAddCarOpen(false);
            }
        };

        document.addEventListener('keydown', handleKeyPress);
        return () => document.removeEventListener('keydown', handleKeyPress);
    }, []);


    const inputConfig = [
        { lable: "Марка", name: "brand", input: "Введите марку", type: "text" },
        { lable: "Модель", name: "model", input: "Введите модель", type: "text" },
        { lable: "Год", name: "yearOfManufacture", input: "Введите год выпуска", type: "text" },
        { lable: "VIN номер", name: "vinNumber", input: "Введите VIN номер", type: "text" },
        { lable: "Государственный номер", name: "stateNumber", input: "Введите номер", type: "text" },
        { lable: "Пробег", name: "mileage", input: "Введите пробег", type: "text" }
    ];

    const changeForm = (e) => {
        const { name, value, type } = e.target;
        setAddForm(prev => ({
            ...prev,
            [name]: type === "number" ? Number(value) : value
        }));
    };

    const submitForm = (e) => {
        e.preventDefault();
        dispatch(createCar(addForm));
        setAddCarOpen(false);
    };

    return (
        <div className="shadow">
            <form onSubmit={submitForm}>
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
                        {inputConfig.map((item) => (
                            <div
                                key={item.name}
                                className='modal-select'>
                                <p className='select-lable'>
                                    {item.lable}
                                </p>
                                <input
                                    className='custom-input-auto'
                                    placeholder={item.input}
                                    type={item.type}
                                    name={item.name}
                                    value={addForm[item.name]}
                                    onChange={changeForm}
                                />
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
                            type='submitForm'
                            className='custom-button'>
                            Добавить
                        </button>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default AddCar;