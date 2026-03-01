import './Registration.css'
import React, { useState, useEffect } from 'react';
import type { AuthModalProps } from "../../types/AuthModalProps.ts";
import { useAuthStore } from "../../store/useAuthStore.ts";
import { useLogin } from "../../hooks/useUser.ts";
import { useRegisterClient } from "../../hooks/useClient.ts";

const Registration: React.FC<AuthModalProps> = ({ registrationIsOpen, setRegistrationIsOpen }) => {
    const setAuth = useAuthStore(state => state.setAuth);

    const { mutate: login, isPending: isLoggingIn } = useLogin();
    const { mutate: registerClient, isPending: isRegistering } = useRegisterClient();

    const [typeMenu, setTypeMenu] = useState<'reg' | 'aut'>('reg');

    const [regForm, setRegForm] = useState({
        userId: 0,
        roleId: 2, //Client
        name: "",
        surname: "",
        phoneNumber: "",
        email: "",
        login: "",
        password: ""
    });

    const[authForm, setAuthForm] = useState({
        login: "",
        password: "",
    });

    useEffect(() => {
        const handleKeyPress = (event: KeyboardEvent) => {
            if (event.key === 'Escape') {
                setRegistrationIsOpen(false);
            }
        };

        document.addEventListener('keydown', handleKeyPress);
        return () => document.removeEventListener('keydown', handleKeyPress);
    },[setRegistrationIsOpen]);

    if (!registrationIsOpen) return null;

    const regInputConfig =[
        { label: "Фамилия", name: "surname", input: "Введите фамилию", type: "text" },
        { label: "Имя", name: "name", input: "Введите имя", type: "text" },
        { label: "Номер телефона", name: "phoneNumber", input: "Введите номер телефона", type: "tel" },
        { label: "Почта", name: "email", input: "Введите почту", type: "email" },
        { label: "Логин", name: "login", input: "Введите логин", type: "text" },
        { label: "Пароль", name: "password", input: "Введите пароль", type: "password" },
    ];

    const autInputConfig =[
        { label: "Логин", name: "login", input: "Введите логин", type: "text" },
        { label: "Пароль", name: "password", input: "Введите пароль", type: "password" },
    ];

    const changeReg = (e: React.ChangeEvent<HTMLInputElement>) => {
        setRegForm(prev => ({ ...prev,[e.target.name]: e.target.value }));
    };

    const changeAuth = (e: React.ChangeEvent<HTMLInputElement>) => {
        setAuthForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const submitReg = (e: React.SyntheticEvent<HTMLFormElement>) => {
        e.preventDefault();
        registerClient(regForm, {
            onSuccess: () => {
                setAuthForm({ login: regForm.login, password: regForm.password });
                setTypeMenu("aut");
            }
        })
    };

    const submitAuth = (e: React.SyntheticEvent<HTMLFormElement>) => {
        e.preventDefault();
        login(authForm, {
            onSuccess: (response) => {
                setAuth({ id: 0, roleId: response.data.roleId, profileId: 0 });
                setRegistrationIsOpen(false);
            }
        })
    }

    const handleOverlayClick = () => setRegistrationIsOpen(false);

    if (typeMenu === 'reg') {
        return (
            <div className='reg-modal-shadow' onClick={handleOverlayClick}>
                <div className='reg-modal' onClick={e => e.stopPropagation()}>
                    <form onSubmit={submitReg}>
                        <h1 className='reg-modal-head'>Регистрация</h1>
                        <div>
                            {regInputConfig.map((item) => (
                                <div className='reg-element' key={item.name}>
                                    <label className='element-name'>{item.label}</label>
                                    <input
                                        className='element-input'
                                        placeholder={item.input}
                                        type={item.type}
                                        name={item.name}
                                        value={regForm[item.name as keyof typeof regForm]}
                                        onChange={changeReg}
                                        required
                                    />
                                </div>
                            ))}
                        </div>
                        <div className='reg-modal-button'>
                            <button
                                className='reg-active-button'
                                type="submit"
                                disabled={isRegistering}
                            >
                                {isRegistering ? 'Регистрация...' : 'Зарегистрироваться'}
                            </button>
                            <button
                                type="button"
                                className='reg-second-button'
                                onClick={() => setTypeMenu('aut')}
                            >
                                Авторизация
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        );
    }

    return (
        <div className='reg-modal-shadow' onClick={handleOverlayClick}>
            <div className='reg-modal' onClick={e => e.stopPropagation()}>
                <form onSubmit={submitAuth}>
                    <h1 className='reg-modal-head'>Авторизация</h1>
                    {autInputConfig.map((item) => (
                        <div className='reg-element' key={item.name}>
                            <label className='element-name'>{item.label}</label>
                            <input
                                className='element-input'
                                type={item.type}
                                name={item.name}
                                placeholder={item.input}
                                value={authForm[item.name as keyof typeof authForm]}
                                onChange={changeAuth}
                                required
                            />
                        </div>
                    ))}

                    <div className='reg-modal-button'>
                        <button
                            className='reg-active-button'
                            type="submit"
                            disabled={isLoggingIn}
                        >
                            {isLoggingIn ? 'Вход...' : 'Войти'}
                        </button>
                        <button
                            type="button"
                            className='reg-second-button'
                            onClick={() => setTypeMenu('reg')}
                        >
                            Зарегистрироваться
                        </button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default Registration;