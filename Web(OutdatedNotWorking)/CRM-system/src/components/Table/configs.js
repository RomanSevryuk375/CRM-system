export const columnsInWorkCars = [
  "id",
  "brand",
  "model",
  "yearOfManufacture",
  "vinNumber",
  "stateNumber",
  "mileage",
];
export const headTextInWorkCars = [
  "№",
  "Производитель",
  "Модель",
  "Год",
  "VIN номер",
  "Номер",
  "Пробег",
];
export const columnsParts = [
  "id",
  "orderId",
  "supplierName",
  "name",
  "article",
  "quantity",
  "unitPrice",
];
export const headTextParts = [
  "№",
  "№ заказ-наряда",
  "Поставщик",
  "Наименование детали",
  "Артикул",
  "Количество",
  "Цена за еденицу",
];

export const columnsBills = [
  "id",
  "orderId",
  "statusId",
  "date",
  "amount",
  "actualBillDate",
];
export const headTextBills = [
  "№",
  "№ заказ-наряда",
  "Статус",
  "Дата выставления счета",
  "Сумма",
  "Дата погашения",
];

export const columnsJournal = ["id", "billId", "date", "amount", "method"];
export const headTextJournal = [
  "№",
  "№ счета",
  "Дата оплаты",
  "Сумма оплаты",
  "Метод оплаты",
];

export const columnsTaxes = ["id", "name", "rate", "type"];
export const headTextTaxes = ["№", "Название", "Ставка", "Тип"];

export const columnsExpenses = [
  "id",
  "date",
  "category",
  "taxName",
  "usedPartInfo",
  "expenseType",
  "sum",
];
export const headTextExpenses = [
  "№",
  "Дата",
  "Категория",
  "№ налога",
  "№ использованной запчасти",
  "Тип",
  "Сумма",
];

export const columnsOreders = [
  "id",
  "statusName",
  "carInfo",
  "date",
  "priority",
];
export const headTextOrders = [
  "№",
  "Статус",
  "Автомобиль",
  "Дата",
  "Приоритет",
];

export const columnsCatalogOfWorks = [
  "id",
  "title",
  "category",
  "description",
  "standardTime",
];
export const headTextCatalogOftWorks = [
  "№",
  "Название работы",
  "Категория",
  "Описание",
  "Нормативное время",
];

export const columnsWorks = [
  "id",
  "orderId",
  "jobName",
  "workerInfo",
  "timeSpent",
  "statusName",
];
export const headTextWorks = [
  "№",
  "№ Заказа",
  "Работа",
  "Работник",
  "Затраченное время",
  "Название статуса",
];

export const columnsSpecializations = ["id", "name"];
export const headTextSpecializations = ["№", "Название"];

export const columnsSuppliers = ["id", "name", "contacts"];
export const headTextSuppliers = ["№", "Название", "Контакты"];

export const columnsWorkProposals = [
  "id",
  "orderId",
  "workName",
  "byWorker",
  "statusName",
  "decisionStatusName",
  "date",
];
export const headTextWorkProposals = [
  "№",
  "№ Заказа",
  "Работа",
  "Работник",
  "Статус",
  "Решение",
  "Дата",
];

export const columnsWorkers = [
  "id",
  "userId",
  "specializationName",
  "name",
  "surname",
  "hourlyRate",
  "phoneNumber",
  "email",
];
export const headTextWorkers = [
  "№",
  "№ пользователя",
  "Специализация",
  "Имя",
  "Фамилия",
  "Почасовая ставка",
  "Номер телефона",
  "Почта",
];

export const columnsClients = [
  "id",
  "user_Id",
  "name",
  "surname",
  "phoneNumber",
  "email",
];
export const headTextClients = [
  "№",
  "№ пользователя",
  "Имя",
  "Фамилия",
  "Телефон",
  "Почта",
];

export const columnsOrdersClient = [
  "id",
  "statusName",
  "carInfo",
  "date",
  "priority",
];
export const headTextOrdersClient = [
  "№",
  "Статус",
  "Автомобиль",
  "Дата",
  "Приоритет",
];

export const columnsHistoryClient = [
  "id",
  "orderId",
  "carInfo",
  "date",
  "serviceSum",
];
export const headTextHistoryClient = [
  "№",
  "№ Заказ-наряда",
  "Автомобиль",
  "Дата",
  "Стоимость",
];

export const columnsBillsClient = [
  "id",
  "orderId",
  "statusId",
  "date",
  "amount",
  "actualBillDate",
  "lastBillDate",
];
export const headTextBillsClient = [
  "№",
  "№ Заказ-наряда",
  "Статус",
  "Дата созадния",
  "Сумма",
  "Дата погашения",
  "Погасить до",
]; //на бэке сделать имя статуса

export const columnsJournalClient = [
  "id",
  "billId",
  "date",
  "amount",
  "method",
];
export const headTextJournalClient = [
  "№",
  "№ Счёта",
  "Дата плтежа",
  "Сумма",
  "Метод",
];

export const columnsWorksForCar = ["id", "jobName", "statusName"];
export const headTextWorksForCar = ["№", "Название рабботы", "Статус"];

export const columnsProposalsForCar = ["id", "workName", "decisionStatusName"];
export const headTextProposalsForCar = ["№", "Название рабботы", "Статус"];
