import Edit from '../assets/Edit.svg';
import Sort from '../assets/Sort.svg'
import './Table.css'

const headText = ['№', 'Клиент', 'Автомобиля', 'Статус', 'Мастер', 'Дата создания', 'Итоговая стоимость'];
const bodyText = [
  { id: 1, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 2, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 3, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
  { id: 4, client: 'Севрюк Роман Петрович', car_id: 23, status: 'принят', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 564 },
  { id: 5, client: 'Пупкин Василий Иванович', car_id: 47, status: 'на диагностике', worker: 'Севрюк Петр Юрьевич', date: '01.01.2025', sum: 2900 },
  { id: 6, client: 'Сяньтянь Лю', car_id: 64, status: 'ожидает согласования', worker: 'Стрелков Григорий Григорьевич', date: '01.01.2025', sum: 5000 },
];

function Table() {
  return (
    <table className='tableMarking'>
      <thead>
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
          <td className='td-id'>{row.id}</td>
          <td className='td-client'>{row.client}</td>
          <td className='td-car_id'>{row.car_id}</td>
          <td className='td-status'>{row.status}</td>
          <td className='td-worker'>{row.worker}</td>
          <td className='td-date'>{row.date}</td>
          <td className='td-sum'>{row.sum}</td>
        </tr>))}
      </tbody>
    </table>
  );
}

export default Table;