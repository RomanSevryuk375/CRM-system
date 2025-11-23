import './Registration.css'
import { useDispatch } from 'react-redux';
import { useState, useEffect } from 'react';
import { createClient } from '../../redux/Actions/clients'
import { loginUser } from '../../redux/Actions/users';

function Registration({ registrationIsOpen, setRegistrationIsOpen }) {
    const dispatch = useDispatch();

    const [regForm, setRegForm] = useState({
        roleId: 2,
        name: "",
        surname: "",
        phoneNumber: "",
        email: "",
        login: "",
        password: ""
    });

    const [authForm, setAuthForm] = useState({
        login: "",
        password: "",
    });

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

    const autInputConfig = [
        { label: "Логин", name: "login", input: "Введите логин", type: "text" },
        { label: "Пароль", name: "password", input: "Введите пароль", type: "password" },
    ];

    const changeReg = (e) => {
        setRegForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const changeAuth = (e) => {
        setAuthForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const submitReg = (e) => {
        e.preventDefault();
        dispatch(createClient(regForm));
        setRegistrationIsOpen(false);
    };

    const submitAuth = (e) => {
        e.preventDefault();
        dispatch(loginUser(authForm.login, authForm.password));
        setRegistrationIsOpen(false);
    }

    switch (typeMenu) {
        case 'reg':
            return (
                <div className='reg-modal-shadow'>
                    <div className='reg-modal'>
                        <form onSubmit={submitReg}>
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
                                            onChange={changeReg}
                                        />
                                    </div>
                                ))}
                            </div>
                            <div className='reg-modal-button'>
                                <button
                                    className='reg-active-button'
                                    type="submitReg">
                                    Зарегистрироваться
                                </button>
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
                    <form onSubmit={submitAuth}>
                        <div className='reg-modal'>
                            <h1 className='reg-modal-head'>Авторизация</h1>
                            {autInputConfig.map((item) => (
                                <div
                                    className='reg-element'
                                    key={item.input}>
                                    <label
                                        className='element-name'
                                        htmlFor="">{item.label}</label>
                                    <input
                                        className='element-input'
                                        type={item.type}
                                        name={item.name}
                                        placeholder={item.input}
                                        value={authForm[item.name]}
                                        onChange={changeAuth}
                                    />
                                </div>
                            ))}

                            <div className='reg-modal-button'>
                                <button
                                    className='reg-active-button'
                                    type="submitAuth"
                                >Войти</button>
                                <button
                                    className='reg-second-button'
                                    onClick={() => setTypeMenu('reg')}>Зарегистрироваться</button>
                            </div>
                        </div>
                    </form>
                </div>
            );
    }

}

export default Registration;