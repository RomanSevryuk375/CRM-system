import Exit from '../../assets/svg/Exit.svg';
import { useEffect, useState } from 'react';
import User from '../../assets/svg/User.svg';
import { useDispatch, useSelector } from 'react-redux';
import { getMyClient } from '../../redux/Actions/clients';

// const manager = {
//     name: 'Петров Петр Петрович',
//     role: 'менеджер',
//     textImg: 'GG'
// };

function Account({ response, registrationIsOpen, setRegistrationIsOpen }) {
    const myClient = useSelector(state => state.clients.myClient);
    const isLoggedIn = useSelector(state => state.users.isLoggedIn);
    const dispatch = useDispatch();

    useEffect(() => {
        if (isLoggedIn) {
            dispatch(getMyClient({}));
        }
    }, [isLoggedIn, dispatch])
    console.log("myClient =", myClient);

    switch (isLoggedIn) {
        case false:
            return (

                /* < div className='profile-mini-cli' > <p className='profile-mini-text-cli'>{manager.textImg}</p></div >
                <div className='profile-user-role-cli'>
                    <h1 className='profile-user-cli'>{manager.name}</h1>
                    <p className='profile-role-cli'>{manager.role}</p>
                </div> */
                <button
                    className='profile-button-cli'
                    onClick={() => setRegistrationIsOpen(!registrationIsOpen)}><img src={User} alt="" /></button>

            )
        case true:
            return (
                <>
                    < div className='profile-mini-cli' > <p className='profile-mini-text-cli'>{myClient[0]?.name?.[0]}{myClient[0]?.surname?.[0]}</p></div >
                    <div className='profile-user-role-cli'>
                        <h1 className='profile-user-cli'>{myClient[0]?.name}</h1>
                        <p className='profile-role-cli'>{myClient[0]?.surname}</p>
                    </div>
                    <button
                        className='profile-button-cli'
                        onClick={() => setRegistrationIsOpen(!registrationIsOpen)}><img src={Exit} alt="" /></button>
                </>
            )
    }

}

export default Account