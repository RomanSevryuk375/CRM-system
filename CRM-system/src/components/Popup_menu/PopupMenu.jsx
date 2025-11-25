import ModalImg from "../../assets/svg/Cross.svg";
import { useCallback, useEffect, useState } from "react";
import "./PopupMenu.css";
import { useDispatch, useSelector } from "react-redux";
import { createCar } from "../../redux/Actions/cars";
import { createOrder } from "../../redux/Actions/order";
import { createPaymentNote } from "../../redux/Actions/paymentNotes";
import {
  getCarFields,
  getMyJournalFields,
  getMyOrderFields,
} from "./fieldConfigs";

const FOOTER_CONTENT_SAVE = ["Сохранить"];

const MODAL_TITLE_CAR = "Создание автомобиля";
const TOOLBAR_CONTENT_CAR = [{ name: "Автомобиль", value: "car" }];

const MODAL_TITLE_MY_ORDERS = "Создание заявки";
const TOOLBAR_CONTENT_MY_ODERS = [{ name: "Заказ-наряд", value: "order" }];

const MODAL_TITLE_MY_JOURNAL = "Создание записи оплаты";
const TOOLBAR_CONTENT_MY_JOURNAL = [{ name: "Запись", value: "paymentNote" }];

const GenericPopupMenu = ({
  modalTitle, // название формы
  toolBarContent, // инпутф или селекты
  footerContent, // кнопки внизу
  fields, // Теперь это массив объектов-описаний полей
  formData, // Текущие данные формы
  onClose,
  setCategoryMenu,
  isOpen,
  handleInputChange, // Обработчик изменения инпутов
  handleSubmit, // Обработчик отправки формы
}) => {
  if (!isOpen) {
    return null;
  }

  return (
    <div className="shadow">
      <div className="main-menu">
        <div className="modal-header">
          <h2 className="modal-title">{modalTitle}</h2>
          <button onClick={onClose} className="exit">
            <img className="modal-img" src={ModalImg} alt="Закрыть" />
          </button>
        </div>
        <div className="toolbar">
          {toolBarContent.map((item) => (
            <button
              key={item.value}
              onClick={() => setCategoryMenu(item.value)}
              className="modal-toolbar-button"
            >
              {item.name}
            </button>
          ))}
        </div>
        <form className="modal-body" onSubmit={handleSubmit}>
          {fields.map((field) => (
            <div key={field.name} className="modal-select">
              <label htmlFor={field.name} className="select-lable">
                {field.label}
              </label>
              {field.type === "select" ? (
                <select
                  id={field.name}
                  name={field.name}
                  className={`custom-select`}
                  value={formData[field.name] || ""}
                  onChange={handleInputChange}
                  disabled={field.readOnly}
                >
                  <option value="" disabled>
                    {field.placeholder ||
                      `Выберите ${field.label.toLowerCase()}`}
                  </option>
                  {field.options.map((option) => (
                    <option key={option.value} value={option.value}>
                      {option.label}
                    </option>
                  ))}
                </select>
              ) : field.type === "textarea" ? (
                <textarea
                  id={field.name}
                  name={field.name}
                  className={`custom-select`}
                  value={formData[field.name] || ""}
                  onChange={handleInputChange}
                  placeholder={field.placeholder}
                  readOnly={field.readOnly}
                />
              ) : (
                <input
                  id={field.name}
                  name={field.name}
                  type={field.type}
                  className={`custom-select`}
                  value={formData[field.name] || ""}
                  onChange={handleInputChange}
                  placeholder={field.placeholder}
                  readOnly={field.readOnly}
                  step={field.type === "number" ? "any" : undefined}
                />
              )}
            </div>
          ))}
          <div className="modal-footer">
            {footerContent.map((buttonText) => (
              <button key={buttonText} className="custom-button" type="submit">
                {buttonText}
              </button>
            ))}
          </div>
        </form>
      </div>
    </div>
  );
};

function PopupMenu({ isOpen, onClose, activeTable, setPage }) {
  const dispatch = useDispatch();
  const clientId = useSelector((state) => state.clients.myClient?.[0]?.id);
  const [categoryMenu, setCategoryMenu] = useState("car"); // Активная вкладка
  const [options, seOptions] = useState({
    cars: [], // Инициализируйте пустыми массивами
    bills: [],
    paymentMethods: [],
  }); // Состояние для динамических опций селектов

  const [addCarForm, setAddCarForm] = useState({
    ownerId: 0,
    brand: "",
    model: "",
    yearOfManufacture: 0,
    vinNumber: "",
    stateNumber: "",
    mileage: 0,
  });
  const [addOrderForm, setAddOrderForm] = useState({
    statusId: 0,
    carId: 0,
    date: "",
    priority: "",
  });
  const [addPaymentNoteForm, setAddPaymentNoteForm] = useState({
    billId: 0,
    date: "",
    amount: 0,
    method: "",
  });

  useEffect(() => {
    if (isOpen && clientId) {
      setAddCarForm({
        ownerId: clientId,
        brand: "",
        model: "",
        yearOfManufacture: 0,
        vinNumber: "",
        stateNumber: "",
        mileage: 0,
      });
    }
  }, [isOpen, clientId]);
  useEffect(() => {
    if (isOpen) {
      setAddOrderForm({
        statusId: 0,
        carId: 0,
        date: "",
        priority: "",
      });
    }
  }, [isOpen]);
  useEffect(() => {
    if (isOpen) {
      setAddPaymentNoteForm({
        billId: 0,
        date: "",
        amount: 0,
        method: "",
      });
    }
  }, [isOpen]);

  // ... (useEffect для загрузки данных и обновления dynamicOptions, если необходимо)
  // Пример:
  useEffect(() => {
    if (isOpen && activeTable === "ordersClient") {
      // Загрузите список автомобилей и обновите setDynamicOptions({ ...prev, cars: fetchedCars })
    }
    if (isOpen && activeTable === "journalClient") {
      // Загрузите счета и методы оплаты
    }
  }, [isOpen, activeTable]);

  const changeCarForm = useCallback((e) => {
    const { name, value, type } = e.target;
    setAddCarForm((prev) => ({
      ...prev,
      [name]: type === "number" ? Number(value) : value,
    }));
  }, []);
  const changeOrderForm = useCallback((e) => {
    const { name, value, type } = e.target;
    setAddOrderForm((prev) => ({
      ...prev,
      [name]: type === "number" ? Number(value) : value,
    }));
  }, []);
  const changePaymenNoteForm = useCallback((e) => {
    const { name, value, type } = e.target;
    setAddPaymentNoteForm((prev) => ({
      ...prev,
      [name]: type === "number" ? Number(value) : value,
    }));
  }, []);

  const submitCarForm = (e) => {
    e.preventDefault();
    dispatch(createCar(addCarForm)).then(() => {
      dispatch(getMyCars(1));
    });
    onClose();
    setPage(1);
  };
  const submitOrderForm = (e) => {
    e.preventDefault();
    dispatch(createOrder(addOrderForm));
    onClose();
    setPage(1);
  };
  const submitPaymentNoteForm = (e) => {
    e.preventDefault();
    const fixedData = {
    ...addPaymentNoteForm,
    date: new Date(addPaymentNoteForm.date).toISOString(), // 🔥 вот фикс
    };
    dispatch(createPaymentNote(fixedData));
    onClose();
    setPage(1);
  };

  let currentFields = [];
  let currentFormData = {};
  let currentHandleInputChange = () => {};
  let currentHandleSubmit = () => {};
  let currentModalTitle = "";
  let currentToolbarContent = []; // Для GenericPopupMenu

  switch (activeTable) {
    case "mainClient":
      currentFields = getCarFields();
      currentFormData = addCarForm;
      currentHandleInputChange = changeCarForm;
      currentHandleSubmit = submitCarForm;
      currentModalTitle = MODAL_TITLE_CAR;
      currentToolbarContent = TOOLBAR_CONTENT_CAR;
      break;
    case "ordersClient":
      currentFields = getMyOrderFields(options);
      currentFormData = addOrderForm;
      currentHandleInputChange = changeOrderForm;
      currentHandleSubmit = submitOrderForm;
      currentModalTitle = MODAL_TITLE_MY_ORDERS;
      currentToolbarContent = TOOLBAR_CONTENT_MY_ODERS;
      break;
    case "journalClient":
      currentFields = getMyJournalFields(options);
      currentFormData = addPaymentNoteForm;
      currentHandleInputChange = changePaymenNoteForm;
      currentHandleSubmit = submitPaymentNoteForm;
      currentModalTitle = MODAL_TITLE_MY_JOURNAL;
      currentToolbarContent = TOOLBAR_CONTENT_MY_JOURNAL;
      break;
    default:
      return null;
  }

  return (
    <GenericPopupMenu
      modalTitle={currentModalTitle}
      toolBarContent={currentToolbarContent}
      footerContent={FOOTER_CONTENT_SAVE}
      fields={currentFields}
      formData={currentFormData}
      onClose={onClose}
      isOpen={isOpen}
      setCategoryMenu={setCategoryMenu}
      handleInputChange={currentHandleInputChange}
      handleSubmit={currentHandleSubmit}
    />
  );
}

export default PopupMenu;
