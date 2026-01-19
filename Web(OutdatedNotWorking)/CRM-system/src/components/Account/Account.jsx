import "./Account.css";
import Exit from "../../assets/svg/Exit.svg";
import { useEffect, useState } from "react";
import User from "../../assets/svg/User.svg";
import { useDispatch, useSelector } from "react-redux";
import { getMyClient } from "../../redux/Actions/clients";
import ExitModal from "../ExitModal/ExitModal";
import { useNavigate } from "react-router-dom";

function Account({ registrationIsOpen, setRegistrationIsOpen }) {
  const myClient = useSelector((state) => state.clients.myClient);
  const myWorker = useSelector((state) => state.workers.myWorker);
  const isLoggedIn = useSelector((state) => state.users.isLoggedIn);
  const usersId = useSelector((state) => state.users.userRoleId);

  const [activeExitMenu, setActiveExitMenu] = useState(false);
  const [activeUserMenu, setActiveUserMenu] = useState(false);

  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    if (isLoggedIn) {
      dispatch(getMyClient({}));
    }
  }, [isLoggedIn, dispatch]);

  console.log("myClient =", myClient);

  const toggleRouting = (id) => {
    switch (id) {
      case 1:
        navigate("/manager-page");
        break;
      case 2:
        navigate("/personal-page");
        break;
      case 3:
        navigate("/worker-page");
        break;
      default:
        break;
    }
  };

  if (!isLoggedIn) {
    return (
      <button
        className="profile-button-cli"
        onClick={() => setRegistrationIsOpen(!registrationIsOpen)}
      >
        <img src={User} alt="" />
      </button>
    );
  }

  return (
    <>
      <div
        className={`Acontainer ${activeUserMenu ? "open" : "close"}`}
        onClick={() => {
          setActiveUserMenu(!activeUserMenu);
        }}
      >
        <div className="Aprofle-content">
          <div className="profile-mini-cli">
            <p className="profile-mini-text-cli">
              {myClient[0]?.name?.[0]}
              {myClient[0]?.surname?.[0]}
            </p>
          </div>
          <div className="profile-user-role-cli">
            <h1 className="profile-user-cli">
              {myClient[0]?.name} {myClient[0]?.surname}
            </h1>
            <p className="profile-role-cli">{myClient[0]?.email}</p>
          </div>
        </div>
        {activeUserMenu && (
          <button
            className="Aprofile-personal-button"
            onClick={() => {
              toggleRouting(usersId);
            }}
          >
            Личный кабнет
          </button>
        )}
      </div>
      <button
        className="profile-button-cli"
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
