import ModalImg from "../../assets/svg/Cross.svg";
import { useCallback, useEffect, useState } from "react";
import "./PopupMenu.css";
import { useDispatch, useSelector } from "react-redux";
import { createCar } from "../../redux/Actions/cars";
import { createOrder } from "../../redux/Actions/order";
import { createPaymentNote } from "../../redux/Actions/paymentNotes";

const FOOTER_CONTENT_SAVE = ["Сохранить"];

const MODAL_TITLE_CAR = "Создание автомобиля";
const TOOLBAR_CONTENT_CAR = [{ name: "Автомобиль", value: "car" }];

const MODAL_TITLE_MY_ORDERS = "Создание заявки";
const TOOLBAR_CONTENT_MY_ODERS = [{ name: "Заказ-наряд", value: "order" }];

const MODAL_TITLE_MY_JOURNAL = "Создание записи оплаты";
const TOOLBAR_CONTENT_MY_JOURNAL = [{ name: "Запись", value: "paymentNote" }];

const getBillFields = (options) => [
  {
    name: "orderId",
    label: "Заказ",
    type: "select",
    options: options.orders || [],
    placeholder: "Выберите заказ",
  },
  {
    name: "statusId",
    label: "Счет",
    type: "select",
    options: [{ value: "", label: "" }],
    placeholder: "Выберите статус",
  },
  {
    name: "date",
    label: "Дата выставления счета",
    type: "date",
    defaultValue: new Date().toISOString().split("T")[0],
  },
];
const getCarFields = () => [
  {
    name: "brand",
    label: "Марка",
    type: "text",
    placeholder: "Введите марку",
  },
  {
    name: "model",
    label: "Модель",
    type: "text",
    placeholder: "Введите модель",
  },
  {
    name: "yearOfManufacture",
    label: "Год",
    type: "number",
    placeholder: "Введите год выпуска",
  },
  {
    name: "vinNumber",
    label: "VIN номер",
    type: "text",
    placeholder: "Введите VIN номер",
  },
  {
    name: "stateNumber",
    label: "Государственный номер",
    type: "text",
    placeholder: "Введите номер",
  },
  {
    name: "mileage",
    label: "Пробег",
    type: "number",
    placeholder: "Введите пробег",
  },
];
const getClientFields = () => [
  {
    name: "roleId",
    label: "Роль",
    type: "select",
    options: [{}],
    placeholder: "Выберите роль",
  },
  {
    name: "name",
    label: "Имя",
    type: "text",
    placeholder: "Введите имя",
  },
  {
    name: "surname",
    label: "Фамилия",
    type: "text",
    placeholder: "Введите фамилию",
  },
  {
    name: "email",
    label: "Почта",
    type: "text",
    placeholder: "Введите почту",
  },
  {
    name: "phomeNumber",
    label: "Номер телефона",
    type: "text",
    placeholder: "Введите номер",
  },
  {
    name: "login",
    label: "Логин",
    type: "text",
    placeholder: "Введите логин",
  },
  {
    name: "password",
    label: "Пароль",
    type: "text",
    placeholder: "Введите пароль",
  },
];
const getExpenseFields = (options) => [
  {
    name: "category",
    label: "Категория расхода",
    type: "select",
    options: [],
    placeholder: "Выберите категорию",
  },
  {
    name: "taxId",
    label: "Налог",
    type: "select",
    options: options.taxes || [],
    placeholder: "Выберите налог",
  },
  {
    name: "usedPartId",
    label: "Запчасть",
    type: "select",
    options: options.usedParts || [],
    placeholder: "Выберите запчасть",
  },
  {
    name: "expenseType",
    label: "Тип",
    type: "select",
    options: [],
    placeholder: "Выберите тип расхода",
  },
  {
    name: "date",
    label: "Дата расхода",
    type: "date",
    defaultValue: new Date().toISOString().split("T")[0],
  },
  {
    name: "sum",
    label: "Сумма",
    type: "number",
    placeholder: "Введите сумму",
  },
];
const getMyOrderFields = (options) => [
  {
    name: "statusId",
    label: "Статус",
    type: "select",
    options: options.statuses || [],
    placeholder: "Выберите статус",
  },
  {
    name: "carId",
    label: "Автомобиль",
    type: "select",
    options: options.cars || [],
    placeholder: "Выберите автомобиль",
  },
  {
    name: "date",
    label: "Дата заказа",
    type: "date",
    defaultValue: new Date().toISOString().split("T")[0],
  },
  {
    name: "priority",
    label: "Приоритет",
    type: "select",
    options: [
      { value: "Стандартный", label: "Стандартный" },
      { value: "Низкий", label: "Низкий" },
    ],
    placeholder: "Выберите приоритет",
  },
];
const getMyJournalFields = (options) => [
  {
    name: "billId",
    label: "Счет",
    type: "select",
    options: options.cars || [],
    placeholder: "Выберите счет",
  },
  {
    name: "method",
    label: "Метод оплаты",
    type: "select",
    options: options.cars || [],
    placeholder: "Выберите метод оплаты",
  },
  {
    name: "amount",
    label: "Пробег",
    type: "number",
    placeholder: "Введите пробег",
  },
  {
    name: "date",
    label: "Дата заказа",
    type: "date",
    defaultValue: new Date().toISOString().split("T")[0],
  },
];
const getSpecializationFields = () => [
  {
    name: "name",
    label: "Название",
    type: "text",
    placeholder: "Введите название",
  },
];
const getSupplierFields = () => [
  {
    name: "name",
    label: "Название",
    type: "text",
    placeholder: "Введите название",
  },
  {
    name: "contacts",
    label: "Контакты",
    type: "text",
    placeholder: "Введите контакты",
  },
];
const getTaxFields = () => [
  {
    name: "name",
    label: "Название",
    type: "text",
    placeholder: "Введите название",
  },
  {
    name: "type",
    label: "Метод налога",
    type: "select",
    options: [],
    placeholder: "Выберите тип налога",
  },
  {
    name: "rate",
    label: "Процент",
    type: "number",
    placeholder: "Введите процент",
  },
];
const getUsedPartFields = (options) => [
  {
    name: "orderId",
    label: "Заказ-наряд",
    type: "select",
    options: options.orders || [],
    placeholder: "Выберите заказ-наряд",
  },
  {
    name: "supplierId",
    label: "Поставщик",
    type: "select",
    options: options.suppliers || [],
    placeholder: "Выберите поставщика",
  },
  {
    name: "name",
    label: "Название",
    type: "text",
    placeholder: "Введите название",
  },
  {
    name: "article",
    label: "Артикул",
    type: "text",
    placeholder: "Введите артикул",
  },
  {
    name: "quantity",
    label: "Количество",
    type: "number",
    placeholder: "Введите количество",
  },
  {
    name: "unitPrice",
    label: "Цена за еденицу",
    type: "number",
    placeholder: "Введите цену",
  },
];
const getWorkFields = (options) => [
  {
    name: "orderId",
    label: "Заказ-наряд",
    type: "select",
    options: options.orders || [],
    placeholder: "Выберите заказ-наряд",
  },
  {
    name: "jobId",
    label: "Заказ-наряд",
    type: "select",
    options: options.cars || [],
    placeholder: "Выберите заказ-наряд",
  },
  {
    name: "workerId",
    label: "Заказ-наряд",
    type: "select",
    options: options.workers || [],
    placeholder: "Выберите заказ-наряд",
  },
  {
    name: "statusId",
    label: "Заказ-наряд",
    type: "select",
    options: options.statuses || [],
    placeholder: "Выберите заказ-наряд",
  },
  {
    name: "timeSpent",
    label: "Количество",
    type: "number",
    placeholder: "Введите количество",
  },
];
const getWorkerFields = (options) => [
  {
    name: "roleId",
    label: "Роль",
    type: "select",
    options: [{}],
    placeholder: "Выберите роль",
  },
  {
    name: "roleId",
    label: "Роль",
    type: "select",
    options: options.specializations || [],
    placeholder: "Выберите роль",
  },
  {
    name: "name",
    label: "Имя",
    type: "text",
    placeholder: "Введите имя",
  },
  {
    name: "surname",
    label: "Фамилия",
    type: "text",
    placeholder: "Введите фамилию",
  },
  {
    name: "yearOfManufacture",
    label: "Год",
    type: "number",
    placeholder: "Введите год выпуска",
  },
  {
    name: "email",
    label: "Почта",
    type: "text",
    placeholder: "Введите почту",
  },
  {
    name: "phomeNumber",
    label: "Номер телефона",
    type: "text",
    placeholder: "Введите номер",
  },
  {
    name: "login",
    label: "Логин",
    type: "text",
    placeholder: "Введите логин",
  },
  {
    name: "password",
    label: "Пароль",
    type: "text",
    placeholder: "Введите пароль",
  },
];
const getWorkProposal = (options) => [
  {
    name: "orderId",
    label: "Заказ-наряд",
    type: "select",
    options: options.orders || [],
    placeholder: "Выберите заказ-наряд",
  },
  {
    name: "workId",
    label: "Работа",
    type: "select",
    options: options.works || [],
    placeholder: "Выберите работу",
  },
  {
    name: "byWorker",
    label: "Работник",
    type: "select",
    options: options.workers || [],
    placeholder: "Выберите работника",
  },
  {
    name: "statusId",
    label: "Статус",
    type: "select",
    options: options.statuses || [],
    placeholder: "Выберите статуса",
  },
  {
    name: "date",
    label: "Дата предложения",
    type: "date",
    defaultValue: new Date().toISOString().split("T")[0],
  },
];
const getWorkType = () => [
  {
    name: "title",
    label: "Название",
    type: "text",
    placeholder: "Введите название",
  },
  {
    name: "category",
    label: "Категория",
    type: "select",
    options: [],
    placeholder: "Выберите категория",
  },
  {
    name: "description",
    label: "Описание",
    type: "text",
    placeholder: "Введите описание",
  },
  {
    name: "standardTime",
    label: "Норма времени",
    type: "number",
    placeholder: "Введите норму времени",
  },
];

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
    dispatch(createPaymentNote(addPaymentNoteForm));
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
