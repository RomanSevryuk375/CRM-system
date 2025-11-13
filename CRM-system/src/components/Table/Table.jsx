import Edit from '../../assets/svg/Edit.svg';
import Sort from '../../assets/svg/Sort.svg';
import Main from '../Main/Main';
import FiltreModal from '../FilreModal/FiltreModal';
import Detailing from '../Detailing/Detailing';
import './Table.css'
import { getCatalogOfWorks } from '../../redux/Actions/catalogOfWorks';
import { useDispatch, useSelector } from 'react-redux';
import { useEffect, useState } from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';

const columnsOreders = ['id', 'client', 'car_id', 'status', 'worker', 'date', 'sum'];
const headTextOrders = ['№', 'Клиент', 'Автомобиля', 'Статус', 'Мастер', 'Дата создания', 'Итоговая стоимость'];
const bodyTextOrders = [
  { id: 1, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 2, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 3, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
  { id: 4, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 5, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 6, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
  { id: 7, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 8, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
];
const columnsClients = ['id', 'surname', 'name', 'patronymic', 'phone_number', 'mail', 'car'];
const headTextClients = ['№', 'Фамилия', 'Имя', 'Отчество', 'Телефон', 'Почта', 'Автомобили'];
const bodyTextClients = [
  { id: 1, surname: 'Смирнов', name: 'Алексей', patronymic: 'Викторович', phone_number: '+79031234567', mail: 'smirnov.av@example.com', car: 'Kia Rio' },
  { id: 2, surname: 'Козлова', name: 'Елена', patronymic: 'Сергеевна', phone_number: '+79169876543', mail: 'kozlova.elena@mail.ru', car: 'Volkswagen Polo' },
  { id: 3, surname: 'Иванов', name: 'Дмитрий', patronymic: 'Андреевич', phone_number: '+79255001122', mail: 'ivanov_dima@yandex.ru', car: 'Toyota Camry' },
  { id: 4, surname: 'Петрова', name: 'Мария', patronymic: 'Олеговна', phone_number: '+79603459900', mail: 'm.petrova@google.com', car: 'Mazda 3' },
  { id: 5, surname: 'Волков', name: 'Игорь', patronymic: 'Николаевич', phone_number: '+79857778899', mail: 'volkov.i.n@example.net', car: 'Skoda Kodiaq' },
  { id: 6, surname: 'Новикова', name: 'Анна', patronymic: 'Ильинична', phone_number: '+79996543210', mail: 'anna_nov@corp.ru', car: 'Hyundai Solaris' },
  { id: 7, surname: 'Кузнецов', name: 'Сергей', patronymic: 'Валентинович', phone_number: '+79152347856', mail: 'sergey.k@auto-service.com', car: 'Lada Vesta' },
  { id: 8, surname: 'Морозова', name: 'Наталья', patronymic: 'Геннадьевна', phone_number: '+79091112233', mail: 'natalya.m@internet.ru', car: 'Nissan Qashqai' },
  { id: 9, surname: 'Зайцев', name: 'Павел', patronymic: 'Евгеньевич', phone_number: '+79774005060', mail: 'pavel.z@gmail.com', car: 'Ford Focus' },
  { id: 10, surname: 'Соколова', name: 'Виктория', patronymic: 'Максимовна', phone_number: '+79587654321', mail: 'v.sokolova@tech.company', car: 'BMW X5' }
];
const columnsWorkers = ['id', 'surname', 'name', 'patronymic', 'specialization', 'hourly_rate', 'phone_number'];
const headTextWorkers = ['№', 'Фамилия', 'Имя', 'Отчество', 'Специализация', 'Почасовая ставка', 'Номер телефона'];
const bodyTextWorkers = [
  { id: 1, surname: 'Петров', name: 'Иван', patronymic: 'Андреевич', specialization: 'Двигатели', hourly_rate: 1500, phone_number: '+79101112233' },
  { id: 2, surname: 'Сидорова', name: 'Ольга', patronymic: 'Николаевна', specialization: 'Электрика/Диагностика', hourly_rate: 1800, phone_number: '+79204445566' },
  { id: 3, surname: 'Ковалев', name: 'Александр', patronymic: 'Сергеевич', specialization: 'Ходовая/Тормоза', hourly_rate: 1200, phone_number: '+79307778899' },
  { id: 4, surname: 'Морозова', name: 'Екатерина', patronymic: 'Ивановна', specialization: 'Шиномонтаж', hourly_rate: 900, phone_number: '+79401010101' },
  { id: 5, surname: 'Зайцев', name: 'Дмитрий', patronymic: 'Олегович', specialization: 'Сварка/Кузовной ремонт', hourly_rate: 2000, phone_number: '+79502020202' },
  { id: 6, surname: 'Куликов', name: 'Виктор', patronymic: 'Петрович', specialization: 'Обслуживание', hourly_rate: 1000, phone_number: '+79603030303' },
  { id: 7, surname: 'Павлова', name: 'Светлана', patronymic: 'Геннадьевна', specialization: 'Малярные работы', hourly_rate: 2200, phone_number: '+79704040404' }

];
const columnsParts = ['id', 'order_id', 'supplier', 'part_name', 'article', 'quantity', 'unit_price'];
const headTextParts = ['№', '№ заказ-наряда', 'Поставщик', 'Наименование детали', 'Артикул', 'Количество', 'Цена за еденицу'];
const bodyTextParts = [
  { id: 1, order_id: 1001, supplier: 'AutoParts-M', part_name: 'Фильтр масляный', article: 'OX123D', quantity: 5, unit_price: 450 },
  { id: 2, order_id: 1002, supplier: 'DistribTech', part_name: 'Колодки тормозные (комплект, перед)', article: 'DB1838', quantity: 2, unit_price: 3200 },
  { id: 3, order_id: 1003, supplier: 'CarSupply', part_name: 'Свеча зажигания', article: 'NGK-BPR6ES', quantity: 8, unit_price: 350 },
  { id: 4, order_id: 1004, supplier: 'AutoParts-M', part_name: 'Ремень ГРМ', article: 'CT1028', quantity: 1, unit_price: 1850 },
  { id: 5, order_id: 1005, supplier: 'DistribTech', part_name: 'Аккумулятор 60 Ач', article: 'VARTA-060', quantity: 1, unit_price: 6500 },
  { id: 6, order_id: 1006, supplier: 'CarSupply', part_name: 'Лампа H7 (ближний свет)', article: 'P43T-12V', quantity: 10, unit_price: 250 },
  { id: 7, order_id: 1007, supplier: 'AutoParts-M', part_name: 'Стойка стабилизатора (передняя)', article: 'JTS525', quantity: 4, unit_price: 950 },
  { id: 8, order_id: 1008, supplier: 'DistribTech', part_name: 'Жидкость тормозная DOT4 (1 л)', article: 'BF4-1L', quantity: 3, unit_price: 700 },
  { id: 9, order_id: 1009, supplier: 'CarSupply', part_name: 'Фильтр воздушный', article: 'ELP3910', quantity: 5, unit_price: 600 },
  { id: 10, order_id: 1010, supplier: 'AutoParts-M', part_name: 'Насос водяной (помпа)', article: 'WP0315', quantity: 1, unit_price: 4500 },
  { id: 11, order_id: 1011, supplier: 'DistribTech', part_name: 'Салонный фильтр (угольный)', article: 'K1211', quantity: 6, unit_price: 800 },
  { id: 12, order_id: 1012, supplier: 'CarSupply', part_name: 'Рычаг подвески (нижний, левый)', article: 'QR3805', quantity: 1, unit_price: 3800 },
  { id: 13, order_id: 1013, supplier: 'AutoParts-M', part_name: 'Масло моторное 5W-40 (4 л)', article: '5W40-SYN', quantity: 7, unit_price: 2100 },
  { id: 14, order_id: 1014, supplier: 'DistribTech', part_name: 'Датчик кислорода (лямбда-зонд)', article: 'LS4001', quantity: 1, unit_price: 5900 },
  { id: 15, order_id: 1015, supplier: 'CarSupply', part_name: 'Антифриз G12+ (концентрат, 1 л)', article: 'G12PLUS-1L', quantity: 4, unit_price: 550 },
  { id: 16, order_id: 1016, supplier: 'AutoParts-M', part_name: 'Наконечник рулевой тяги', article: 'TRW-900', quantity: 2, unit_price: 1150 }
];
const columnsBills = ['id', 'order_id', 'status', 'invoice_date', 'sum']
const headTextBills = ['№', '№ заказ-наряда', 'Статус', 'Дата выставления счета', 'Сумма']
const bodyTextBills = [
  { id: 1, order_id: 1001, status: 'Оплачен', invoice_date: '2024-01-15', sum: 2250 },
  { id: 2, order_id: 1002, status: 'Ожидает оплаты', invoice_date: '2024-01-16', sum: 6400 },
  { id: 3, order_id: 1003, status: 'Оплачен', invoice_date: '2024-01-17', sum: 2800 },
  { id: 4, order_id: 1004, status: 'Отменен', invoice_date: '2024-01-18', sum: 1850 },
  { id: 5, order_id: 1005, status: 'Оплачен', invoice_date: '2024-01-19', sum: 6500 },
  { id: 6, order_id: 1006, status: 'Ожидает оплаты', invoice_date: '2024-01-20', sum: 2500 },
  { id: 7, order_id: 1007, status: 'Оплачен', invoice_date: '2024-01-21', sum: 3800 },
  { id: 8, order_id: 1008, status: 'Оплачен', invoice_date: '2024-01-22', sum: 2100 },
  { id: 9, order_id: 1009, status: 'Ожидает оплаты', invoice_date: '2024-01-23', sum: 3000 },
  { id: 10, order_id: 1010, status: 'Оплачен', invoice_date: '2024-01-24', sum: 4500 },
  { id: 11, order_id: 1011, status: 'Отменен', invoice_date: '2024-01-25', sum: 4800 },
  { id: 12, order_id: 1012, status: 'Оплачен', invoice_date: '2024-01-26', sum: 3800 },
  { id: 13, order_id: 1013, status: 'Ожидает оплаты', invoice_date: '2024-01-27', sum: 14700 },
  { id: 14, order_id: 1014, status: 'Оплачен', invoice_date: '2024-01-28', sum: 5900 },
  { id: 15, order_id: 1015, status: 'Оплачен', invoice_date: '2024-01-29', sum: 2200 },
  { id: 16, order_id: 1016, status: 'Ожидает оплаты', invoice_date: '2024-01-30', sum: 2300 }
];
const columnsJournal = ['id', 'bill_id', 'paiment_date', 'sum', 'paiment_method'];
const headTextJournal = ['№', '№ счета', 'Дата оплаты', 'Сумма оплаты', 'Метод оплаты']
const bodyTextJournal = [
  { id: 1, bill_id: 1001, paiment_date: '2024-01-16', sum: 2250, paiment_method: 'Банковская карта' },
  { id: 2, bill_id: 1003, paiment_date: '2024-01-18', sum: 2800, paiment_method: 'Наличные' },
  { id: 3, bill_id: 1005, paiment_date: '2024-01-20', sum: 6500, paiment_method: 'Банковский перевод' },
  { id: 4, bill_id: 1007, paiment_date: '2024-01-22', sum: 3800, paiment_method: 'Банковская карта' },
  { id: 5, bill_id: 1008, paiment_date: '2024-01-23', sum: 2100, paiment_method: 'Наличные' },
  { id: 6, bill_id: 1010, paiment_date: '2024-01-25', sum: 4500, paiment_method: 'Банковский перевод' },
  { id: 7, bill_id: 1012, paiment_date: '2024-01-27', sum: 3800, paiment_method: 'Банковская карта' },
  { id: 8, bill_id: 1014, paiment_date: '2024-01-29', sum: 5900, paiment_method: 'Наличные' },
  { id: 9, bill_id: 1015, paiment_date: '2024-01-30', sum: 2200, paiment_method: 'Банковская карта' },
  { id: 10, bill_id: 1002, paiment_date: '2024-01-31', sum: 6400, paiment_method: 'Банковский перевод' },
  { id: 11, bill_id: 1006, paiment_date: '2024-02-01', sum: 2500, paiment_method: 'Наличные' },
  { id: 12, bill_id: 1009, paiment_date: '2024-02-02', sum: 3000, paiment_method: 'Банковская карта' },
  { id: 13, bill_id: 1013, paiment_date: '2024-02-03', sum: 14700, paiment_method: 'Банковский перевод' },
  { id: 14, bill_id: 1016, paiment_date: '2024-02-04', sum: 2300, paiment_method: 'Наличные' }
];
const columnsTaxes = ['id', 'name', 'procent', 'type'];
const headTextTaxes = ['№', 'Название', 'Ставка', 'Тип'];
const bodyTextTaxes = [
  { id: 1, name: 'Налог на прибыль', procent: 18.00, type: '%' },
  { id: 2, name: 'НДС (основная ставка)', procent: 20.00, type: '%' },
  { id: 3, name: 'НДС (льготная ставка)', procent: 0.00, type: '%' },
  { id: 4, name: 'Налог на недвижимость', procent: 1.00, type: '%' },
  { id: 5, name: 'Земельный налог', procent: 0.35, type: '%' },
  { id: 6, name: 'Социальные взносы (ФСЗН)', procent: 34.00, type: '%' },
  { id: 7, name: 'Подоходный налог (сотрудники)', procent: 13.00, type: '%' }
];
const columnsExpenses = ['id', 'date', 'category', 'tax_id', 'used_parts_id', 'type', 'sum'];
const headTextExpenses = ['№', 'Дата', 'Категория', '№ налога', '№ использованной запчасти', 'Тип', 'Сумма']
const bodyTextExpenses = [
  { id: 1, date: '2024-01-15', category: 'Запчасти', tax_id: 2, used_parts_id: 1, type: 'Расход', sum: 2250 },
  { id: 2, date: '2024-01-16', category: 'Зарплата', tax_id: 6, used_parts_id: null, type: 'Расход', sum: 150000 },
  { id: 3, date: '2024-01-17', category: 'Коммунальные услуги', tax_id: 4, used_parts_id: null, type: 'Расход', sum: 25000 },
  { id: 4, date: '2024-01-18', category: 'Запчасти', tax_id: 2, used_parts_id: 2, type: 'Расход', sum: 6400 },
  { id: 5, date: '2024-01-19', category: 'Аренда', tax_id: 4, used_parts_id: null, type: 'Расход', sum: 80000 },
  { id: 6, date: '2024-01-20', category: 'Запчасти', tax_id: 2, used_parts_id: 3, type: 'Расход', sum: 2800 },
  { id: 7, date: '2024-01-21', category: 'Реклама', tax_id: 2, used_parts_id: null, type: 'Расход', sum: 15000 },
  { id: 8, date: '2024-01-22', category: 'Запчасти', tax_id: 2, used_parts_id: 4, type: 'Расход', sum: 1850 },
  { id: 9, date: '2024-01-23', category: 'Обслуживание оборудования', tax_id: 2, used_parts_id: null, type: 'Расход', sum: 12000 },
  { id: 10, date: '2024-01-24', category: 'Запчасти', tax_id: 2, used_parts_id: 5, type: 'Расход', sum: 6500 },
  { id: 11, date: '2024-01-25', category: 'Транспортные расходы', tax_id: 2, used_parts_id: null, type: 'Расход', sum: 8000 },
  { id: 12, date: '2024-01-26', category: 'Запчасти', tax_id: 2, used_parts_id: 6, type: 'Расход', sum: 2500 },
  { id: 13, date: '2024-01-27', category: 'Налоги', tax_id: 1, used_parts_id: null, type: 'Расход', sum: 45000 },
  { id: 14, date: '2024-01-28', category: 'Запчасти', tax_id: 2, used_parts_id: 7, type: 'Расход', sum: 3800 },
  { id: 15, date: '2024-01-29', category: 'Хозяйственные расходы', tax_id: 2, used_parts_id: null, type: 'Расход', sum: 5000 },
  { id: 16, date: '2024-01-30', category: 'Запчасти', tax_id: 2, used_parts_id: 8, type: 'Расход', sum: 2100 }
];
const columnsWorks = ['id', 'title', 'category', 'description', 'standardTime'];
const headTextWorks = ['№', 'Название работы', 'Категория', 'Описание', 'Нормативное время'];

const GenericTable = ({
  headText,
  bodyText,
  columns,
  activeFoolMenu,
  activeDetailing,
  setActiveDetailing,
  nextHandler,
  hasMore,}) => {
  return (
    <InfiniteScroll
      dataLength={bodyText.length}
      next={nextHandler}
      hasMore={hasMore}

      scrollableTarget="container"
    >
      <div id = "container" className={`table-container ${activeFoolMenu ? 'enable' : 'disable'}`}>
        <table className='tableMarking'>
          <thead className='thead'>
            <tr>
              <th className='start-th-button'></th>
              {headText.map((item) => (
                <th key={item} className='сolumn-names'>
                  <div className='сolumn-elements'>
                    <p className='names'>
                      {item}
                    </p>
                    <button className='button-sort'>
                      <img className='button-sort-img' src={Sort} alt="Сортировать" />
                    </button>
                  </div>
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {bodyText.map((row, index) => (
              <tr
                key={row.id}
                className={`table-row ${index % 2 === 0 ? 'even' : 'odd'}`}
                onClick={() => { setActiveDetailing(!activeDetailing) }}>
                <td>
                  <button className={`td-button ${index % 2 === 0 ? 'even' : 'odd'}`}>
                    <img src={Edit} alt="Редактировать" />
                  </button>
                </td>
                {columns.map((columnKey, columnIndex) => (
                  <td key={columnIndex} className='td-sum'>
                    {row[columnKey]}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </InfiniteScroll>
  );
}

function Table({ activeTable, activeFoolMenu }) {
  const [activeDetailing, setActiveDetailing] = useState(false);

  const catalogOfWorks = useSelector(state => state.catalogOfWorks.catalogOfWorks);
  const totalCatalog = useSelector(state => state.catalogOfWorks.totalCatalog);
  const dispatch = useDispatch();
  const [page, setPage] = useState(1);

  const nextHendler = () => {
    if (catalogOfWorks.length < totalCatalog) {
        setPage(prev => prev + 1);
    } else {
        console.log("No more data to load");
    }
  };

  useEffect(() => {
    if (activeTable === 'works') {
      dispatch(getCatalogOfWorks(page));
    }
  }, [page, activeTable, dispatch]);

  const hasMoreItems = catalogOfWorks.length < totalCatalog;
  switch (activeTable) {
    case 'main':
      return (
        <Main />
      );
    case 'orders':
      return (
        <>
          <GenericTable
            headText={headTextOrders}
            bodyText={bodyTextOrders}
            columns={columnsOreders}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );
    case 'clients':
      return (
        <>
          <GenericTable
            headText={headTextClients}
            bodyText={bodyTextClients}
            columns={columnsClients}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );
    case 'workers':
      return (
        <>
          <GenericTable
            headText={headTextWorkers}
            bodyText={bodyTextWorkers}
            columns={columnsWorkers}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );
    case 'works':
      return (
        <>
          <GenericTable
            headText={headTextWorks}
            bodyText={catalogOfWorks || []}
            columns={columnsWorks}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHendler}
            hasMore={hasMoreItems}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );
    case 'parts':
      return (
        <>
          <GenericTable
            headText={headTextParts}
            bodyText={bodyTextParts}
            columns={columnsParts}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );
    case 'bills':
      return (
        <>
          <GenericTable
            headText={headTextBills}
            bodyText={bodyTextBills}
            columns={columnsBills}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );
    case 'journal':
      return (
        <>
          <GenericTable
            headText={headTextJournal}
            bodyText={bodyTextJournal}
            columns={columnsJournal}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );
    case 'taxes':
      return (
        <>
          <GenericTable
            headText={headTextTaxes}
            bodyText={bodyTextTaxes}
            columns={columnsTaxes}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );
    case 'expenses':
      return (
        <>
          <GenericTable
            headText={headTextExpenses}
            bodyText={bodyTextExpenses}
            columns={columnsExpenses}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
          />
          <Detailing
            activeDetailing={activeDetailing}
            activeFoolMenu={activeFoolMenu} />
        </>
      );

    default:
    return (
      <h1>Error</h1>
    );
  }
}

export default Table;

