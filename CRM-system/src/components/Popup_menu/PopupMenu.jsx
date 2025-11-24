import ModalImg from '../../assets/svg/Cross.svg';
import { useCallback, useEffect, useState } from 'react';
import './PopupMenu.css'
import { useDispatch, useSelector } from 'react-redux';
import { createCar } from '../../redux/Actions/cars';

const MODAL_TITLE = 'Создание автомобиля';
const TOOLBAR_CONTENT = [
    { name: 'Автомобиль', value: 'car' },
    // { name: 'Работы', value: 'works' },
    // { name: 'Запчасти', value: 'parts' },
    // { name: 'Итог', value: 'result' }
];
const FOOTER_CONTENT = ['Сохранить'];

// --- Функции для определения полей каждой вкладки ---
// Каждая функция принимает `options` (для динамических данных селектов)
// и `formData` (для полей, значения которых могут зависеть от других полей формы)
const getCarFields = () => [
    {
        name: 'brand',
        label: 'Марка',
        type: 'text',
        placeholder: 'Введите марку',
    },
    {
        name: 'model',
        label: 'Модель',
        type: 'text',
        placeholder: 'Введите модель',
    },
    {
        name: 'yearOfManufacture',
        label: 'Год',
        type: 'number',
        placeholder: 'Введите год выпуска',
    },
    {
        name: 'vinNumber',
        label: 'VIN номер',
        type: 'text',
        placeholder: 'Введите VIN номер',
    },
    {
        name: 'stateNumber',
        label: 'Государственный номер',
        type: 'text',
        placeholder: 'Введите номер',
    },
    {
        name: 'mileage',
        label: 'Пробег',
        type: 'number',
        placeholder: 'Введите пробег',
    },
];
// {
//     name: 'car',
//     label: 'Автомобиль',
//     type: 'select',
//     options: options.cars || [], // Динамические опции для селекта
//     placeholder: 'Выберите автомобиль',
//     validation: { required: true },
// },
// {
//     name: 'orderDate', // Изменено с 'date' для избежания конфликтов с типом 'date'
//     label: 'Дата заказа',
//     type: 'date',
//     validation: { required: true },
//     defaultValue: new Date().toISOString().split('T')[0], // Значение по умолчанию: сегодняшняя дата
// },
// {
//     name: 'status',
//     label: 'Статус',
//     type: 'select',
//     options: options.statuses || [], // Динамические опции для селекта
//     placeholder: 'Выберите статус',
//     validation: { required: true },
// },
// const getWorksFields = (options) => [
//     {
//         name: 'workType',
//         label: 'Работы',
//         type: 'select',
//         options: options.workTypes || [],
//         placeholder: 'Выберите тип работы',
//         validation: { required: true },
//     },
//     {
//         name: 'master',
//         label: 'Мастер',
//         type: 'select',
//         options: options.masters || [],
//         placeholder: 'Выберите мастера',
//         validation: { required: true },
//     },
//     {
//         name: 'workDescription',
//         label: 'Описание работ',
//         type: 'textarea', // Пример текстового поля
//         placeholder: 'Подробное описание выполненных работ',
//         validation: { maxLength: 500 },
//     },
//     {
//         name: 'standardTime',
//         label: 'Нормативное время (часы)',
//         type: 'number',
//         validation: { min: 0.1 },
//     }
// ];

// const getPartsFields = (options) => [
//     {
//         name: 'supplier',
//         label: 'Поставщик',
//         type: 'select',
//         options: options.suppliers || [],
//         placeholder: 'Выберите поставщика',
//         validation: { required: true },
//     },
//     {
//         name: 'partName',
//         label: 'Наименование',
//         type: 'text',
//         placeholder: 'Название детали',
//         validation: { required: true, minLength: 2 },
//     },
//     {
//         name: 'article',
//         label: 'Артикул',
//         type: 'text',
//         placeholder: 'Артикул детали',
//         validation: { required: true, minLength: 3 },
//     },
//     {
//         name: 'quantity',
//         label: 'Количество',
//         type: 'number',
//         validation: { required: true, min: 1 },
//         defaultValue: 1,
//     },
//     {
//         name: 'unitPrice',
//         label: 'Цена за единицу (BYN)',
//         type: 'number',
//         validation: { required: true, min: 0.01 },
//     },
// ];

// // Поля для вкладки "Итог" могут быть только для чтения и вычисляться на основе других полей formData
// const getResultFields = (formData) => [
//     {
//         name: 'workCost',
//         label: 'Стоимость работ (BYN)',
//         type: 'number',
//         readOnly: true, // Только для чтения
//         value: formData.workCost || 0, // Пример: вычисляемое значение
//     },
//     {
//         name: 'partsCost',
//         label: 'Стоимость запчастей (BYN)',
//         type: 'number',
//         readOnly: true,
//         value: formData.partsCost || 0, // Пример: вычисляемое значение
//     },
//     {
//         name: 'totalCost',
//         label: 'Итоговая стоимость (BYN)',
//         type: 'number',
//         readOnly: true,
//         value: (formData.workCost || 0) + (formData.partsCost || 0), // Пример: вычисляемая сумма
//     },
//     {
//         name: 'finalStatus',
//         label: 'Статус заказа',
//         type: 'select',
//         options: [{ value: 'pending', label: 'Ожидает' }, { value: 'completed', label: 'Завершен' }], // Пример статических опций
//         validation: { required: true },
//     },
// ];

// Карта функций для получения полей по категории
const fieldDefinitionsMap = {
    car: getCarFields, // Изменено с 'basic' на 'car'
};


const GenericPopupMenu = ({
    modalTitle, // название формы 
    toolBarContent, // инпутф или селекты 
    footerContent, // кнопки внизу
    fields, // Теперь это массив объектов-описаний полей
    formData, // Текущие данные формы
    onClose, //
    setCategoryMenu, //
    isOpen, //
    handleInputChange, // Обработчик изменения инпутов
    handleSubmit, // Обработчик отправки формы
}) => {
    if (!isOpen) {
        return null;
    }

    return (
        <div className='shadow'>
            <div className='main-menu'>
                <div className='modal-header'>
                    <h2 className='modal-title'>{modalTitle}</h2>
                    <button
                        onClick={onClose}
                        className='exit-button' // Изменено имя класса
                        aria-label="Закрыть модальное окно" // Для доступности
                    >
                        <img className='modal-img' src={ModalImg} alt="Закрыть" />
                    </button>
                </div>

                <div className='toolbar'> {/* Изменено имя класса */}
                    {toolBarContent.map((item) => (
                        <button
                            key={item.value}
                            onClick={() => setCategoryMenu(item.value)}
                            className='modal-toolbar-button'
                            // Для доступности: указывает, что кнопка выбрана
                            aria-pressed={formData.categoryMenu === item.value}
                        >
                            {item.name}
                        </button>
                    ))}
                </div>

                {/* Форма для обработки отправки */}
                <form className='modal-body' onSubmit={handleSubmit}>
                    {fields.map((field) => (
                        <div key={field.name} className='form-field-group'>
                            <label htmlFor={field.name} className='field-label'>
                                {field.label}
                            </label>
                            {field.type === 'select' ? (
                                <select
                                    id={field.name}
                                    name={field.name}
                                    className={`custom-select`}
                                    value={formData[field.name] || ''}
                                    onChange={handleInputChange}
                                    disabled={field.readOnly}
                                >
                                    <option value="" disabled>{field.placeholder || `Выберите ${field.label.toLowerCase()}`}</option>
                                    {field.options.map((option) => (
                                        <option key={option.value} value={option.value}>
                                            {option.label}
                                        </option>
                                    ))}
                                </select>
                            ) : field.type === 'textarea' ? (
                                <textarea
                                    id={field.name}
                                    name={field.name}
                                    className={`custom-textarea`}
                                    value={formData[field.name] || ''}
                                    onChange={handleInputChange}
                                    placeholder={field.placeholder}
                                    readOnly={field.readOnly}
                                />
                            ) : (
                                <input
                                    id={field.name}
                                    name={field.name}
                                    type={field.type}
                                    className={`custom-input`}
                                    value={formData[field.name] || ''}
                                    onChange={handleInputChange}
                                    placeholder={field.placeholder}
                                    readOnly={field.readOnly}
                                    step={field.type === 'number' ? 'any' : undefined} // Позволяет вводить десятичные дроби для чисел
                                />
                            )}
                        </div>
                    ))}
                    <div className='modal-footer'> {/* Изменено имя класса */}
                        {footerContent.map((buttonText) => (
                            <button
                                key={buttonText}
                                className='custom-button'
                                type='submit' // Передаем текст кнопки для определения действия
                            >
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
    const clientId = useSelector(state => state.clients.myClient?.[0]?.id);
    const [categoryMenu, setCategoryMenu] = useState('car'); // Активная вкладка
    const [formData, setFormData] = useState({}); // Состояние данных формы
    const [dynamicOptions, setDynamicOptions] = useState({}); // Состояние для динамических опций селектов

    const [addForm, setAddForm] = useState({
        ownerId: 0,
        brand: "",
        model: "",
        yearOfManufacture: 0,
        vinNumber: "",
        stateNumber: "",
        mileage: 0
    });

    useEffect(() => {
        if (isOpen && clientId) {
            setAddForm({
                ownerId: clientId,
                brand: "",
                model: "",
                yearOfManufacture: 0,
                vinNumber: "",
                stateNumber: "",
                mileage: 0
            });
        }
    }, [isOpen, clientId]);

    const changeForm = useCallback((e) => {
        const { name, value, type } = e.target;
        setAddForm(prev => ({
            ...prev,
            [name]: type === "number" ? Number(value) : value
        }));
    }, []);

    const submitForm = (e) => {
        e.preventDefault();
        dispatch(createCar(addForm)).then(() => {
            dispatch(getMyCars(1));
        });
        onClose();
        setPage(1);
    };

    const currentFields = fieldDefinitionsMap[categoryMenu]
        ? fieldDefinitionsMap[categoryMenu]() // Вызываем функцию для получения полей
        : [];

    return (
        <GenericPopupMenu
            modalTitle={MODAL_TITLE}
            toolBarContent={TOOLBAR_CONTENT}
            footerContent={FOOTER_CONTENT}
            fields={currentFields} // Передаем динамически определенные поля
            formData={addForm}
            onClose={onClose}
            isOpen={isOpen}
            setCategoryMenu={setCategoryMenu}
            handleInputChange={changeForm}
            handleSubmit={submitForm}
        />
    );
}

export default PopupMenu;

// поправить разметку
// поправить селекты
// прописать поля селектов массивом полей
// ошибки валидации данных



