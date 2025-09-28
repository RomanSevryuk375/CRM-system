import Edit from '../../assets/svg/Edit.svg';
import Sort from '../../assets/svg/Sort.svg';
import FiltreModal from '../FilreModal/FiltreModal';
import './Table.css'

const headText = ['№', 'Клиент', 'Автомобиля', 'Статус', 'Мастер', 'Дата создания', 'Итоговая стоимость'];
const bodyText = [
  { id: 1, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 2, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 3, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
  { id: 4, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 5, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 6, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
  { id: 7, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 8, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 9, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
  { id: 10, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 11, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 12, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
  { id: 13, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 14, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 15, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
  { id: 16, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 17, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 18, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
];


function Table() {
  return (
    <>
      {/* <FiltreModal />   */}
      <div className='table-container'>
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
                      <img className='button-sort-img' src={Sort} alt="" />
                    </button>
                  </div>
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {bodyText.map((row, index) => (<tr key={row.id} className={`table-row ${index % 2 === 0 ? 'even' : 'odd'}`}>
              <td><button className={`td-button ${index % 2 === 0 ? 'even' : 'odd'}`}><img src={Edit} alt="" /></button></td>
              <td className='td-sum'>{row.id}</td>
              <td className='td-sum'>{row.client}</td>
              <td className='td-sum'>{row.car_id}</td>
              <td className='td-sum'>{row.status}</td>
              <td className='td-sum'>{row.worker}</td>
              <td className='td-sum'>{row.date}</td>
              <td className='td-sum'>{row.sum}</td>
            </tr>))}
          </tbody>
        </table>
      </div>
    </>
  );
}

export default Table;