import {
  DEFAULT_DATE_VALUE,
  EXPENSE_CATEGORY_OPTIONS,
  EXPENSE_TYPE_OPTIONS,
  PAYMENT_METHOD_OPTIONS,
  PRIORITY_OPTIONS,
  TAX_TYPE_OPTIONS,
  WORK_CATEGORY_OPTIONS,
} from "../Popup_menu/enums";

export const getCarFields = () => [
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
export const getCarWorkerFields = () => [
  {
    name: "mileage",
    label: "Пробег",
    type: "number",
    placeholder: "Введите пробег",
  },
];
export const getClientFields = () => [
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
];
export const getExpenseFields = (options) => [
  {
    name: "category",
    label: "Категория расхода",
    type: "select",
    options: EXPENSE_CATEGORY_OPTIONS,
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
    options: EXPENSE_TYPE_OPTIONS,
    placeholder: "Выберите тип расхода",
  },
  {
    name: "date",
    label: "Дата расхода",
    type: "date",
    defaultValue: DEFAULT_DATE_VALUE,
  },
  {
    name: "sum",
    label: "Сумма",
    type: "number",
    placeholder: "Введите сумму",
  },
];
export const getMyOrderFields = (options) => [
  {
    name: "statusId",
    label: "Статус",
    type: "select",
    options: options.statuses || [],
    placeholder: "Выберите статус",
  },
  {
    name: "date",
    label: "Дата заказа",
    type: "date",
    defaultValue: DEFAULT_DATE_VALUE,
  },
  {
    name: "priority",
    label: "Приоритет",
    type: "select",
    options: PRIORITY_OPTIONS,
    placeholder: "Выберите приоритет",
  },
];

export const getMyJournalFields = (options) => [
  {
    name: "method",
    label: "Метод оплаты",
    type: "select",
    options: PAYMENT_METHOD_OPTIONS,
    placeholder: "Выберите метод оплаты",
  },
  {
    name: "amount",
    label: "Сумма",
    type: "number",
    placeholder: "Введите сумму",
  },
  {
    name: "date",
    label: "Дата заказа",
    type: "date",
    defaultValue: DEFAULT_DATE_VALUE,
  },
];
export const getSpecializationFields = () => [
  {
    name: "name",
    label: "Название",
    type: "text",
    placeholder: "Введите название",
  },
];
export const getSupplierFields = () => [
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
export const getTaxFields = () => [
  {
    name: "name",
    label: "Название",
    type: "text",
    placeholder: "Введите название",
  },
  {
    name: "type",
    label: "Тип налога",
    type: "select",
    options: TAX_TYPE_OPTIONS,
    placeholder: "Выберите тип налога",
  },
  {
    name: "rate",
    label: "Процент",
    type: "number",
    placeholder: "Введите процент",
  },
];
export const getUsedPartFields = (options) => [
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
export const getWorkFields = (options) => [
  {
    name: "workerId",
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
    placeholder: "Выберите статус",
  },
  {
    name: "timeSpent",
    label: "Количество",
    type: "number",
    placeholder: "Введите количество",
  },
];
export const getWorkWorkerFields = (options) => [
  {
    name: "statusId",
    label: "Статус",
    type: "select",
    options: options.statuses || [],
    placeholder: "Выберите статус",
  },
  {
    name: "timeSpent",
    label: "Количество",
    type: "number",
    placeholder: "Введите количество",
  },
];
export const getWorkerFields = (options) => [
  {
    name: "specializationId",
    label: "Специализация",
    type: "select",
    options: options.specializations,
    placeholder: "Выберите специализацию",
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
    name: "hourlyRate",
    label: "Поочасовая ставка",
    type: "number",
    placeholder: "Введите почасовую ставку",
  },
  {
    name: "email",
    label: "Почта",
    type: "text",
    placeholder: "Введите почту",
  },
  {
    name: "phoneNumber",
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
export const getWorkProposalFields = (options) => [
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
    defaultValue: DEFAULT_DATE_VALUE,
  },
];
export const getWorkTypeFields = () => [
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
    options: WORK_CATEGORY_OPTIONS,
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
