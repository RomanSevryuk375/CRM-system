import Edit from '../../assets/svg/Edit.svg';
import Sort from '../../assets/svg/Sort.svg';
import Main from '../Main/Main';
import FiltreModal from '../FilreModal/FiltreModal';
import Detailing from '../Detailing/Detailing';
import './Table.css'
import { getCatalogOfWorks } from '../../redux/Actions/catalogOfWorks';
import { useDispatch, useSelector } from 'react-redux';
import { useEffect, useRef, useState } from 'react';
import InfiniteScroll from 'react-infinite-scroll-component';
import { getOrdersWithInfo } from '../../redux/Actions/order';
import { getClients } from '../../redux/Actions/clients';
import { getWorkerWithInfo } from '../../redux/Actions/workers';
import { getUsedPartsWithInfo } from '../../redux/Actions/usedParts';
import { getBills } from '../../redux/Actions/bills';
import { getPaymentNotes } from '../../redux/Actions/paymentNotes';
import { getTaxes } from '../../redux/Actions/taxes';
import { getExpensesWithInwo } from '../../redux/Actions/expenses';

const columnsParts = ['id', 'orderId', 'supplierName', 'name', 'article', 'quantity', 'unitPrice'];
const headTextParts = ['№', '№ заказ-наряда', 'Поставщик', 'Наименование детали', 'Артикул', 'Количество', 'Цена за еденицу'];

const columnsBills = ['id', 'orderId', 'statusId', 'date', 'amount', 'actualBillDate']
const headTextBills = ['№', '№ заказ-наряда', 'Статус', 'Дата выставления счета', 'Сумма', 'Дата погашения']

const columnsJournal = ['id', 'billId', 'date', 'amount', 'method'];
const headTextJournal = ['№', '№ счета', 'Дата оплаты', 'Сумма оплаты', 'Метод оплаты']

const columnsTaxes = ['id', 'name', 'rate', 'type'];
const headTextTaxes = ['№', 'Название', 'Ставка', 'Тип'];

const columnsExpenses = ['id', 'date', 'method', 'tax_id', 'used_parts_id', 'type', 'sum'];
const headTextExpenses = ['№', 'Дата', 'Категория', '№ налога', '№ использованной запчасти', 'Тип', 'Сумма']

const columnsOreders = ['id', 'statusName', 'carInfo', 'date', 'priority'];
const headTextOrders = ['№', 'Статус', 'Автомобиль', 'Дата', 'Приоритет'];

const columnsWorks = ['id', 'title', 'category', 'description', 'standardTime'];
const headTextWorks = ['№', 'Название работы', 'Категория', 'Описание', 'Нормативное время'];

const columnsWorkers = ['id', 'userId', 'specializationName', 'name', 'surname', 'hourlyRate', 'phoneNumber', 'email'];
const headTextWorkers = ['№', '№ пользователя', 'Специализация', 'Имя', 'Фамилия', 'Почасовая ставка', 'Номер телефона', 'Почта'];

const columnsClients = ['id', 'user_Id', 'name', 'surname', 'phoneNumber', 'email'];
const headTextClients = ['№', '№ пользователя', 'Имя', 'Фамилия', 'Телефон', 'Почта'];


const GenericTable = ({
  headText,
  bodyText,
  columns,
  activeFoolMenu,
  activeDetailing,
  setActiveDetailing,
  nextHandler,
  hasMore, }) => {
  return (
    <InfiniteScroll
      dataLength={bodyText.length}
      next={nextHandler}
      hasMore={hasMore}
      scrollableTarget="container"
    >
      <div id="container" className={`table-container ${activeFoolMenu ? 'enable' : 'disable'}`}>
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
  const orders = useSelector(state => state.orders.orders);
  const clients = useSelector(state => state.clients.clients);
  const workers = useSelector(state => state.workers.workersWithInfo);
  const parts = useSelector(state => state.usedParts.usedPartsWithInfo);
  const bills = useSelector(state => state.bills.bills);
  const journal = useSelector(state => state.paymentNotes.paymentNotes);
  const taxes = useSelector(state => state.taxes.taxes);
  const expenses = useSelector(state => state.expenses.expenses);

  const totalCatalog = useSelector(state => state.catalogOfWorks.totalCatalog);
  const totalOrders = useSelector(state => state.orders.totalOrders);
  const totalClients = useSelector(state => state.clients.totalClients);
  const totalWorkers = useSelector(state => state.workers.workersWithInfoTotal);
  const totalParts = useSelector(state => state.usedParts.usedPartsWithInfoTotal);
  const totalBills = useSelector(state => state.bills.billsTotal);
  const totalJournal = useSelector(state => state.paymentNotes.paymentNotesTotal);
  const totalTaxes = useSelector(state => state.taxes.totalTaxes);
  const totalExpenses = useSelector(state => state.expenses.expensesTotal);

  const dispatch = useDispatch();
  const [page, setPage] = useState(1);
  const isLoadingRef = useRef(false);
  const isSwitchingTable = useRef(false);

  useEffect(() => {
    isSwitchingTable.current = true;
    setPage(1);
  }, [activeTable]);


  useEffect(() => {
    if (isSwitchingTable.current) {
      if (page === 1) {
        isSwitchingTable.current = false;
      } else {
        return;
      }
    }

    if (isLoadingRef.current) return;
    isLoadingRef.current = true;

    let action;
    switch (activeTable) {
      case "orders":
        action = getOrdersWithInfo(page);
        break;

      case "clients":
        action = getClients(page);
        break;

      case "workers":
        action = getWorkerWithInfo(page);
        break;

      case "works":
        action = getCatalogOfWorks(page);
        break;

      case "parts":
        action = getUsedPartsWithInfo(page);
        break;

      case "bills":
        action = getBills(page);
        break;

      case "journal":
        action = getPaymentNotes(page);
        break;

      case "taxes":
        action = getTaxes(page);
        break;

      case "expenses":
        action = getExpensesWithInwo(page);
        break;

      default:
        isLoadingRef.current = false;
        return;
    }

    dispatch(action)
      .finally(() => {
        isLoadingRef.current = false;
      });

  }, [page, activeTable, dispatch]);

  const nextHandler = () => {
    if (isLoadingRef.current) return;

    if (activeTable === "orders") {
      if (orders.length >= totalOrders) return;
    }

    if (activeTable === "clients") {
      if (clients.length >= totalClients) return;
    }

    if (activeTable === "workers") {
      if (workers.length >= totalWorkers) return;
    }

    if (activeTable === "works") {
      if (catalogOfWorks.length >= totalCatalog) return;
    }

    if (activeTable === "parts") {
      if (parts.length >= totalParts) return;
    }

    if (activeTable === "bills") {
      if (bills.length >= totalBills) return;
    }

    if (activeTable === "journal") {
      if (journal.length >= totalJournal) return;
    }

    if (activeTable === "taxes") {
      if (taxes.length >= totalTaxes) return;
    }

    if (activeTable === "expenses") {
      if (expenses.length >= totalExpenses) return;
    }

    setPage(prev => prev + 1);
  };

  
  const hasMoreItemsForOrders = orders.length < totalOrders;
  const hasMoreItemsForClients = clients.length < totalClients;
  const hasMoreItemsForWorkers = workers.length < totalWorkers;
  const hasMoreItemsForCatalog = catalogOfWorks.length < totalCatalog;
  const hasMoreItemsForParts = parts.length < totalParts;
  const hasMoreItemsForBills = bills.length < totalBills;
  const hasMoreItemsForJournal = journal.length < totalJournal;
  const hasMoreItemsForTaxes = taxes.length < totalTaxes;
  const hasMoreItemsForExpeses = expenses.length < totalExpenses;

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
            bodyText={orders || []}
            columns={columnsOreders}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForOrders}
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
            bodyText={clients || []}
            columns={columnsClients}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForClients}
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
            bodyText={workers || []}
            columns={columnsWorkers}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForWorkers}
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
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForCatalog}
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
            bodyText={parts || []}
            columns={columnsParts}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForParts}
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
            bodyText={bills || []}
            columns={columnsBills}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForBills}
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
            bodyText={journal || []}
            columns={columnsJournal}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForJournal}
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
            bodyText={taxes || []}
            columns={columnsTaxes}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForTaxes}
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
            bodyText={expenses}
            columns={columnsExpenses}
            activeFoolMenu={activeFoolMenu}
            activeDetailing={activeDetailing}
            setActiveDetailing={setActiveDetailing}
            nextHandler={nextHandler}
            hasMore={hasMoreItemsForExpeses}
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

