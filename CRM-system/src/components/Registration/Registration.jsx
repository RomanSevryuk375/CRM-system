import './Registration.css'
import { useDispatch } from 'react-redux';
import { useState, useEffect } from 'react';
import { createClient } from '../../redux/Actions/clients'

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

    const [typeMenu, setTypeMenu] = useState('reg');

    useEffect(() => {
        const handleKeyPress = (event) => {
            if (event.key === 'Escape') {
                setRegistrationIsOpen(false);
            }
        };

        document.addEventListener('keydown', handleKeyPress);
    }, []);

    if (!registrationIsOpen) return null;

    const regInputConfig = [
        { label: "Фамилия", name: "surname", input: "Введите фамилию", type: "text" },
        { label: "Имя", name: "name", input: "Введите имя", type: "text" },
        { label: "Номер телефона", name: "phoneNumber", input: "Введите номер телефона", type: "tel" },
        { label: "Почта", name: "email", input: "Введите почту", type: "email" },
        { label: "Логин", name: "login", input: "Введите логин", type: "text" },
        { label: "Пароль", name: "password", input: "Введите пароль", type: "password" },
    ];

    const change = (e) => {
        setRegForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const submit = (e) => {
        e.preventDefault();
        dispatch(createClient(regForm));
        setRegistrationIsOpen(false);
        setResponse(!response);
    };

    switch (typeMenu) {
        case 'reg':
            return (
                <div className='reg-modal-shadow'>
                    <div className='reg-modal'>
                        <form onSubmit={submit}>
                            <h1 className='reg-modal-head'>Регистрация</h1>
                            <div>
                                {regInputConfig.map((item) => (
                                    <div
                                        className='reg-element'
                                        key={item.name}>
                                        <label
                                            className='element-name'
                                            htmlFor="">{item.label}
                                        </label>
                                        <input
                                            className='element-input'
                                            placeholder={item.input}
                                            type={item.type}
                                            name={item.name}
                                            value={regForm[item.name]}
                                            onChange={change}
                                        />
                                    </div>
                                ))}
                            </div>
                            <div className='reg-modal-button'>
                                <button className='reg-active-button'
                                    type="submit"
                                // onClick={() => {
                                //     setResponse(!response);
                                //     setRegistrationIsOpen(!registrationIsOpen);
                                // }} //временное решенеие для завершения верстки 
                                >Зарегистрироваться</button>
                                <button
                                    className='reg-second-button'
                                    onClick={() => setTypeMenu('aut')}>Авторизация</button>
                            </div>
                        </form>
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

export default Registration;