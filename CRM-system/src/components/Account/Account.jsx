import Exit from '../../assets/svg/Exit.svg';
import { useEffect, useState } from 'react';
import User from '../../assets/svg/User.svg';
import { useDispatch, useSelector } from 'react-redux';
import { getMyClient } from '../../redux/Actions/clients';
import { logoutUser } from '../../redux/Actions/users';
import ExitModal from '../ExitModal/ExitModal';


function Account({ registrationIsOpen, setRegistrationIsOpen }) {
    const myClient = useSelector(state => state.clients.myClient);
    const isLoggedIn = useSelector(state => state.users.isLoggedIn);

    const [activeExitMenu, setActiveExitMenu] = useState(false)

    const dispatch = useDispatch();

    useEffect(() => {
        if (isLoggedIn) {
            dispatch(getMyClient({}));
        }
    }, [isLoggedIn, dispatch])
    console.log("myClient =", myClient);

    if (!isLoggedIn) {
        return (
            <button
                className='profile-button-cli'
                onClick={() => setRegistrationIsOpen(!registrationIsOpen)}
            >
                <img src={User} alt="" />
            </button>
        );
    }

   return (
        <>
            <div className='profile-mini-cli'>
                <p className='profile-mini-text-cli'>
                    {myClient[0]?.name?.[0]}{myClient[0]?.surname?.[0]}
                </p>
            </div>

            <div className='profile-user-role-cli'>
                <h1 className='profile-user-cli'>{myClient[0]?.name} {myClient[0]?.surname}</h1>
                <p className='profile-role-cli'>{myClient[0]?.email}</p>
            </div>

            <button
                className='profile-button-cli'
                // onClick={() => dispatch(logoutUser())}
                onClick={() => setActiveExitMenu(true)}
            >
                <img src={Exit} alt="" />
            </button>
            <ExitModal 
                activeExitMenu={activeExitMenu}
                setActiveExitMenu={setActiveExitMenu}
            />
        </>
    );

}

export default Account;

