import {
  DEFAULT_DATE_VALUE,
  EXPENSE_CATEGORY_OPTIONS,
  EXPENSE_TYPE_OPTIONS,
  PAYMENT_METHOD_OPTIONS,
  PRIORITY_OPTIONS,
  TAX_TYPE_OPTIONS,
  WORK_CATEGORY_OPTIONS,
} from "./enums";

export const getBillFields = (options) => [
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
    options: options.statuses || [],
    placeholder: "Выберите статус",
  },
  {
    name: "date",
    label: "Дата выставления счета",
    type: "date",
    defaultValue: DEFAULT_DATE_VALUE,
  },
];

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
export const getClientFields = () => [
  {
    name: "roleId",
    label: "Роль",
    type: "select",
    options: [{ value: "2", label: "Клиент" }],
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
    name: "billId",
    label: "Счет",
    type: "select",
    options: [{value: 2, label: "test"}], //options.bills || []
    placeholder: "Выберите счет",
  },
  {
    name: "method",
    label: "Метод оплаты",
    type: "select",
    options: PAYMENT_METHOD_OPTIONS,
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
    label: "Метод налога",
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
export const getWorkFields = (options) => [
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
export const getWorkerFields = (options) => [
  {
    name: "roleId",
    label: "Роль",
    type: "select",
    options: [{ value: "2", label: "Клиент" }],
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
export const getWorkProposalFields = (options) => [
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
