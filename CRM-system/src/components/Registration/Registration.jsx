import { useDispatch } from 'react-redux';
import './Registration.css'
import { useState, useEffect } from 'react';

const regInputConfig = [
    { label: "Фамилия", input: "Введите фамилию", type: "text" },
    { label: "Имя", input: "Введите имя", type: "text" },
    { label: "Номер телефона", input: "Введите номер телефона", type: "tel" },
    { label: "Почта", input: "Введите почту", type: "email" },
    { label: "Пароль", input: "Введите пароль", type: "password" }
];

const autInputConfig = [
    { label: "Логин", input: "Введите логин", type: "text" },
    { label: "Пароль", input: "Введите пароль", type: "password" }
];

function Registration({ registrationIsOpen, setRegistrationIsOpen, response, setResponse }) {
    const dispatch = useDispatch();
    const [regForm, setRegForm] = useState({
        roleId: 2,
        name: "",
        surname: "",
        phoneNumber: "",
        email: "",
        login: "",
        password: ""
    })

    if (!registrationIsOpen) {
        return null;
    }

    useEffect(() => {
        const handleKeyPress = (event) => {
            if (event.key === 'Escape') {
                setRegistrationIsOpen(!registrationIsOpen)
            }
        };

        document.addEventListener('keydown', handleKeyPress);
    }, []);

    const [typeMenu, setTypeMenu] = useState('reg')
    switch (typeMenu) {
        case 'reg':
            return (
                <div className='reg-modal-shadow'>
                    <div className='reg-modal'>
                        <h1 className='reg-modal-head'>Регистрация</h1>
                        <div>
                            {regInputConfig.map((item) => (
                                <div
                                    className='reg-element'
                                    key={item.input}>
                                    <label
                                        className='element-name'
                                        htmlFor="">{item.label}</label>
                                    <input
                                        className='element-input'
                                        type={item.type} placeholder={item.input} />
                                </div>
                            ))}
                        </div>
                        <div className='reg-modal-button'>
                            <button className='reg-active-button'
                                onClick={() => {
                                    setResponse(!response);
                                    setRegistrationIsOpen(!registrationIsOpen);
                                }} //временное решенеие для завершения верстки 
                            >Зарегистрироваться</button>
                            <button
                                className='reg-second-button'
                                onClick={() => setTypeMenu('aut')}>Авторизация</button>
                        </div>
                    </div>
                </div>
            );
        case 'aut':
            return (
                <div className='reg-modal-shadow'>
                    <div className='reg-modal'>
                        <h1 className='reg-modal-head'>Авторизация</h1>
                        <div>
                            {autInputConfig.map((item) => (
                                <div
                                    className='reg-element'
                                    key={item.input}>
                                    <label
                                        className='element-name'
                                        htmlFor="">{item.label}</label>
                                    <input
                                        className='element-input'
                                        type={item.type} placeholder={item.input} />
                                </div>
                            ))}
                        </div>
                        <div className='reg-modal-button'>
                            <button
                                className='reg-active-button'
                                onClick={() => {
                                    setResponse(!response);
                                    setRegistrationIsOpen(!registrationIsOpen);
                                }} //временное решенеие для завершения верстки 
                            >Войти</button>
                            <button
                                className='reg-second-button'
                                onClick={() => setTypeMenu('reg')}>Зарегистрироваться</button>
                        </div>
                    </div>
                </div>
            );
    }

}

export default Registration