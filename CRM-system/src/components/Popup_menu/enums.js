export const DEFAULT_DATE_VALUE = new Date().toISOString().split("T")[0];

export const PRIORITY_OPTIONS = [
  { value: "Стандартный", label: "Стандартный" },
  { value: "Низкий", label: "Низкий" },
  { value: "Повышенный", label: "Повышенный" },
];

export const EXPENSE_CATEGORY_OPTIONS = [
  { value: "Зарплата", label: "Зарплата" },
  { value: "Запчасти", label: "Запчасти" },
  { value: "Аренда", label: "Аренда" },
  { value: "Коммунальные", label: "Коммунальные" },
  { value: "Реклама", label: "Реклама" },
  { value: "Оборудование", label: "Оборудование" },
  { value: "Хозяйственные", label: "Хозяйственные" },
  { value: "Налоги", label: "Налоги" },
  { value: "Ремонт", label: "Ремонт" },
  { value: "Связь", label: "Связь" },
  { value: "Страхование", label: "Страхование" },
];

export const EXPENSE_TYPE_OPTIONS = [
  { value: "Переменный", label: "Переменный" },
  { value: "Постоянный", label: "Постоянный" },
  { value: "Капитальный", label: "Капитальный" },
];

export const TAX_TYPE_OPTIONS = [
  { value: "республиканский", label: "республиканский" },
  { value: "внебюджетный", label: "внебюджетный" },
  { value: "местный", label: "местный" },
  { value: "целевой", label: "целевой" },
];

export const WORK_CATEGORY_OPTIONS = [
  { value: "Диагностика и обслуживание", label: "Диагностика и обслуживание" },
  {
    value: "Двигатель и система выпуска",
    label: "Двигатель и система выпуска",
  },
  { value: "Ходовая часть и рулевое", label: "Ходовая часть и рулевое" },
  { value: "Тормозная система", label: "Тормозная система" },
  { value: "Трансмиссия", label: "Трансмиссия" },
  { value: "Шины и колеса", label: "Шины и колеса" },
  { value: "Электрика и электроника", label: "Электрика и электроника" },
  { value: "Кузовные работы", label: "Кузовные работы" },
];

export const PAYMENT_METHOD_OPTIONS = [
  { value: "Наличными", label: "Наличными" },
  { value: "Картой", label: "Картой" },
  { value: "ЕРИП", label: "ЕРИП" },
  { value: "Рассрочка", label: "Рассрочка" },
  { value: "Другое", label: "Другое" },
];
