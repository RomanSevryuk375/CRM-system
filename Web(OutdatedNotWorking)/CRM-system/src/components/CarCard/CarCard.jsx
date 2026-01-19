import "./CarCard.css";

import Car from "../../assets/svg/Car.svg";
import Vin from "../../assets/svg/Vin.svg";
import StateNumber from "../../assets/svg/GosNumber.svg";
import Milage from "../../assets/svg/Probeg.svg";
import StatusMin from "../../assets/svg/StatusMin.svg";
import { useDispatch, useSelector } from "react-redux";
import { useEffect, useState } from "react";
import { deleteCar, getMyCars } from "../../redux/Actions/cars";
import InfiniteScroll from "react-infinite-scroll-component";
import Table from "../Table/Table";
import { acceptWorkProposal, getWorkProposalForCar, rejectWorkProposal } from "../../redux/Actions/workProposals";
import { getWorksForCar } from "../../redux/Actions/works";
import { getBillForCar } from "../../redux/Actions/bills";

function CarCard({ isMod, setIsMod, page, setPage }) {
  const myCars = useSelector((state) => state.cars.myCars);
  const myCarsTotal = useSelector((state) => state.cars.myCarsTotal);
  const isLoggedIn = useSelector((state) => state.users.isLoggedIn);
  const billForCar = useSelector((state) => state.bills.billForCar); 
  const [activeDetailing, setActiveDetailing] = useState(null);
  console.log(activeDetailing);
  const dispatch = useDispatch();
  const [expandedCardId, setExpandedCardId] = useState(null);

  const toggleDetails = (cardId) => {
    setExpandedCardId((prevId) => (prevId === cardId ? null : cardId));
  };

  useEffect(() => {
    if (isLoggedIn) {
      dispatch(getMyCars(page));
    }
  }, [isLoggedIn, page, isMod, dispatch]);

  const handleDeleteCar = (carId) => {
    dispatch(deleteCar(carId));
    setIsMod(!isMod);

    setPage(1);
  };

  const nextHandler = () => {
    if (myCars.length >= myCarsTotal) return;

    setPage((prev) => prev + 1);
  };

  const hasMore = myCarsTotal !== null && myCars.length < myCarsTotal;
  if (!isLoggedIn) return null;
  return (
    <InfiniteScroll
      className="CCcustom-scroll"
      dataLength={myCars.length}
      next={nextHandler}
      hasMore={hasMore}
      scrollableTarget="container"
    >
      <div className="car-card-screen" id="container">
        {myCars.map((item) => (
          <div
            key={item.id}
            className={`car-card-content ${
              expandedCardId === item.id ? "expanded" : "folded"
            }`}
          >
            <div
              className={`CCfolded ${
                expandedCardId === item.id ? "expanded" : "folded"
              }`}
            >
              <div
                className={`car-buttons ${
                  expandedCardId === item.id ? "expanded" : "folded"
                }`}
              >
                <h1 className="car-buttons-model">
                  {item.brand} {item.model}
                </h1>
                <img
                  className={`car-buttons-img ${
                    expandedCardId === item.id ? "expanded" : "folded"
                  }`}
                  src={Car}
                />
                <div
                  className={`car-buttons-button-container ${
                    expandedCardId === item.id ? "expanded" : "folded"
                  }`}
                >
                  <button
                    className="car-buttons-button"
                    onClick={() => {
                      dispatch(getBillForCar(item.id)),
                      dispatch(getWorkProposalForCar(item.id)),
                      dispatch(getWorksForCar(item.id)),
                      toggleDetails(item.id)}}
                  >
                    {expandedCardId === item.id ? "Скрыть" : "Подробнее"}
                  </button>
                  <button
                    className={`car-buttons-delete ${
                      expandedCardId === item.id ? "expanded" : "folded"
                    }`}
                    onClick={() => handleDeleteCar(item.id)}
                  >
                    Удалить
                  </button>
                </div>
              </div>
              <div className="car-info">
                <h1
                  className={`car-info-model ${
                    expandedCardId === item.id ? "expanded" : "folded"
                  }`}
                >
                  Технические характеристики
                </h1>
                <div
                  className={`car-info-cards ${
                    expandedCardId === item.id ? "expanded" : "folded"
                  }`}
                >
                  <div className="card-info-card">
                    <img className="card-info-img" src={Vin} />
                    <h2 className="card-info-label">VIN-номер</h2>
                    <span className="card-info-span">{item.vinNumber}</span>
                  </div>
                  <div className="card-info-card">
                    <img className="card-info-img" src={StateNumber} />
                    <h2 className="card-info-label">Гос. номер</h2>
                    <span className="card-info-span">{item.stateNumber}</span>
                  </div>
                  <div className="card-info-card">
                    <img className="card-info-img" src={Milage} />
                    <h2 className="card-info-label">Пробег</h2>
                    <span className="card-info-span">{item.mileage}</span>
                  </div>
                  <div className="card-info-card">
                    <img className="card-info-img" src={StatusMin} />
                    <h2 className="card-info-label">ID автомобиля</h2>
                    <span className="card-info-span">{item.id}</span>
                  </div>
                </div>
              </div>
            </div>
            {expandedCardId === item.id && (
              <div className="car-details-expanded">
                <div>
                  <h1 className="CCexpanded-table-label">Работы</h1>
                  <div className="CCtable">
                    <Table 
                      activeTable={"worksForCar"}
                      activeDetailing={activeDetailing}
                      setActiveDetailing={setActiveDetailing}
                      setPage={setPage}
                      page={page}
                    />
                  </div>
                  <h1 className="CCexpanded-table-label">Предложения</h1>
                  <div className="CCtable">
                    <Table 
                      activeTable={"proposalsForCar"}
                      activeDetailing={activeDetailing}
                      setActiveDetailing={setActiveDetailing}  
                      setPage={setPage}
                      page={page}                  
                    />
                  </div>
                </div>
                <div className="CCexpanded-info">
                  <div className="CCexpanded-block-info">
                    <h1 className="CCexpanded-info-label">Финансы</h1>
                    <div>
                      <p className="CCexpanded-info-details">Общая сумма</p>
                      <h6 className="CCexpanded-info-info">{billForCar?.[0]?.amount ?? "—"}</h6>
                      <button className="car-buttons-button">Оплатить</button>
                    </div>
                  </div>
                  <div className="CCexpanded-block-info">
                    <h1 className="CCexpanded-info-label">Контакты</h1>
                    <div>
                      <p className="CCexpanded-info-details">
                        Телефон менеджера
                      </p>
                      <h6 className="CCexpanded-info-info">80447591940</h6>
                      <p className="CCexpanded-info-details">
                        Телефон закрепленного мастера
                      </p>
                      <h6 className="CCexpanded-info-info">80447591940</h6>
                    </div>
                  </div>
                  <div className="CCexpanded-block-info">
                    <h1 className="CCexpanded-info-label">Решение</h1>
                    <div>
                      <p className="CCexpanded-info-details">
                        Выбранное предлоение
                      </p>
                      <h6 className="CCexpanded-info-info">{activeDetailing ?? "-"}</h6>
                      <div className="CCexpanded-prop-buttons">
                        <div>
                          <button 
                            className="car-buttons-button"
                            onClick={() => {dispatch(acceptWorkProposal(activeDetailing))}}
                          >
                            Принять
                          </button>
                        </div>
                        <div>
                          <button 
                            className="car-buttons-button"
                            onClick={() => {dispatch(rejectWorkProposal(activeDetailing))}}
                          >
                            Отклонить
                          </button>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            )}
          </div>
        ))}
      </div>
    </InfiniteScroll>
  );
}

export default CarCard;
