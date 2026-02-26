export const ABSENCE_TYPES: Record<number, { label: string, color: string }> = {
    1: { label: 'Больничный', color: 'White' },
    2: { label: 'Прогул', color: 'Red' },
    3: { label: 'Отпуск', color: 'Green' },
    4: { label: 'Отгул', color: 'Orange' },
}

export const BILL_STATUSES: Record<number, { label: string, color: string }> = {
    1: { label: 'Оплачен', color: 'Green' },
    2: { label: 'Неоплачен', color: 'Red' },
    3: { label: 'Чатично оплачен', color: 'Orange' },
}

export const CAR_STATUSES: Record<number, { label: string, color: string }> = {
    1: { label: 'В работе', color: 'Blue' },
    2: { label: 'Не в работе', color: 'Grey' },
}

export const EXPENSE_TYPES: Record<number, { label: string, color: string }> = {
    1: { label: 'Запчасти и материалы', color: 'Orange' },
    2: { label: 'Аренда и коммунальные услуги', color: 'Purple' },
    3: { label: 'Инструменты и оборудование', color: 'Cyan' },
    4: { label: 'IT и связь', color: 'Blue' },
    5: { label: 'Маркетинг', color: 'Pink' },
    6: { label: 'Документация и лицензии', color: 'Brown' },
    7: { label: 'Логистика и транспорт', color: 'Yellow' },
    8: { label: 'Офис и расходные материалы', color: 'Grey' },
    9: { label: 'Финансовые расходы', color: 'Green' },
}

export const NOTIFICATION_STATUSES: Record<number, { label: string, color: string }> = {
    1: { label: 'Отправлено', color: 'Blue' },
    2: { label: 'Прочитано', color: 'Green' },
}

export const NOTIFICATION_TYPES: Record<number, { label: string, color: string }> = {
    1: { label: 'Рабочий процесс заказа', color: 'Blue' },
    2: { label: 'Платежи и финансы', color: 'Green' },
    3: { label: 'Инвентарь и запчасти', color: 'Orange' },
    4: { label: 'Планирование обслуживания', color: 'Purple' },
    5: { label: 'Системное', color: 'Red' },
    6: { label: 'Клиентское', color: 'Cyan' },
}

export const ORDER_PRIORITIES: Record<number, { label: string, color: string }> = {
    1: { label: 'Низкий', color: 'Green' },
    2: { label: 'Средний', color: 'Orange' },
    3: { label: 'Высокий', color: 'Red' },
}

export const ORDER_STATUSES: Record<number, { label: string, color: string }> = {
    1: { label: 'Ожидание', color: 'Grey' },
    2: { label: 'Принят', color: 'Blue' },
    3: { label: 'Диагностика', color: 'Purple' },
    4: { label: 'Закрыт', color: 'Black' },
    5: { label: 'В процессе', color: 'Orange' },
    6: { label: 'Завершен', color: 'Green' },
}

export const PAYMENT_METHODS: Record<number, { label: string, color: string }> = {
    1: { label: 'Карта', color: 'Blue' },
    2: { label: 'Наличные', color: 'Green' },
    3: { label: 'ЕРИП', color: 'Cyan' },
}

export const PROPOSAL_STATUSES: Record<number, { label: string, color: string }> = {
    1: { label: 'Ожидание', color: 'Orange' },
    2: { label: 'Принято', color: 'Green' },
    3: { label: 'Отклонено', color: 'Red' },
}

export const ROLES: Record<number, { label: string, color: string }> = {
    1: { label: 'Менеджер', color: 'Red' },
    2: { label: 'Клиент', color: 'Green' },
    3: { label: 'Работник', color: 'Blue' },
}

export const SPECIALIZATIONS: Record<number, { label: string, color: string }> = {
    1: { label: 'Механик', color: 'Blue' },
    2: { label: 'Моторист', color: 'Red' },
    3: { label: 'Специалист по трансмиссии', color: 'Orange' },
    4: { label: 'Специалист по подвеске', color: 'Brown' },
    5: { label: 'Специалист по тормозам', color: 'Cyan' },
    6: { label: 'Автоэлектрик', color: 'Yellow' },
    7: { label: 'Диагност', color: 'Purple' },
    8: { label: 'Кузовщик', color: 'Grey' },
    9: { label: 'Маляр', color: 'Pink' },
}

export const TAX_TYPES: Record<number, { label: string, color: string }> = {
    1: { label: 'Налог на прибыль', color: 'Red' },
    2: { label: 'НДС', color: 'Blue' },
    3: { label: 'Налог на имущество', color: 'Orange' },
    4: { label: 'Земельный налог', color: 'Brown' },
    5: { label: 'Социальные взносы', color: 'Green' },
    6: { label: 'Экологический налог', color: 'Cyan' },
    7: { label: 'Местные сборы', color: 'Grey' },
}

export const WORK_STATUSES: Record<number, { label: string, color: string }> = {
    1: { label: 'В процессе', color: 'Orange' },
    2: { label: 'В ожидании', color: 'Grey' },
    3: { label: 'Завершено', color: 'Green' },
}