import "./Account.css";
import Exit from "../../assets/svg/Exit.svg";
import { useState } from "react";
import User from "../../assets/svg/User.svg";
import ExitModal from "../ExitModal/ExitModal.tsx";
import { useNavigate } from "react-router-dom";
import {useAuthStore} from "../../store/useAuthStore.ts";
import {useClient} from "../../hooks/useClient.ts";
import * as React from "react";
import type {AuthModalProps} from "../../types/AuthModalProps.ts";


const Account: React.FC<AuthModalProps> = ({ registrationIsOpen, setRegistrationIsOpen })=> {
  const navigate = useNavigate();

  const { user } = useAuthStore();
  const { data: myClient } = useClient(user?.profileId ?? 0);

  const [activeExitMenu, setActiveExitMenu] = useState(false);
  const [activeUserMenu, setActiveUserMenu] = useState(false);

  const toggleRouting = (roleId: number) => {
    switch (roleId) {
      case 1: navigate("/manager-page"); break;
      case 2: navigate("/personal-page"); break;
      case 3: navigate("/worker-page"); break;
    }
  };

  if (!user) {
    return (
      <button
        className="profile-button-cli"
        onClick={() => setRegistrationIsOpen(!registrationIsOpen)}
      >
        <img src={User} alt="Login" />
      </button>
    );
  }

  const initials = myClient
      ? `${myClient.data.name?.[0] ?? ''}
         ${myClient.data.surname?.[0] ?? ''}`
      : "??";

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
            <p className="profile-mini-text-cli">{initials.toUpperCase()}</p>
          </div>
          <div className="profile-user-role-cli">
            <h1 className="profile-user-cli">{initials.toUpperCase()}</h1>
            <p className="profile-role-cli">{myClient?.data.email}</p>
          </div>
        </div>
        {activeUserMenu && (
          <button
            className="Aprofile-personal-button"
            onClick={(e) => {
              e.stopPropagation();
              toggleRouting(user.roleId);
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
