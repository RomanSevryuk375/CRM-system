import Exit from '../../assets/svg/Exit.svg';
import { useState } from 'react';
import User from '../../assets/svg/User.svg';

const manager = {
    name: 'Петров Петр Петрович',
    role: 'менеджер',
    textImg: 'GG'
};

function Account({response, registrationIsOpen, setRegistrationIsOpen}) {
    switch (response) {
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
                    < div className='profile-mini-cli' > <p className='profile-mini-text-cli'>{manager.textImg}</p></div >
                    <div className='profile-user-role-cli'>
                        <h1 className='profile-user-cli'>{manager.name}</h1>
                        <p className='profile-role-cli'>{manager.role}</p>
                    </div>
                    <button
                        className='profile-button-cli'
                        onClick={() => setRegistrationIsOpen(!registrationIsOpen)}><img src={Exit} alt="" /></button>
                </>
            )
    }

}

export default Account