import './Table.css'
import Edit from '../assets/Edit.svg';

function Table() {
  return (
    <table className='tableMarking'>
      <thead>
        <th className='start-th-button'></th>
        <th>№</th>
        <th>Клиент</th>
        <th>ID автомобиля</th>
        <th>Статус</th>
        <th>Мастер</th>
        <th>Итоговая стоимость</th>
      </thead>
      <tbody>
        <tr>
          <th><button className='th-button'><img src={Edit} alt="" /></button></th>
          <td>1</td>
          <td>9.1</td>
          <td>Зелёная миля</td>
          <td>1999</td>
          <td>Зелёная миля</td>
          <td>1999</td>
        </tr>
        <tr>
          <th><button className='th-button'><img src={Edit} alt="" /></button></th>
          <td>2</td>
          <td>9.1</td>
          <td>Побег из Шоушенка</td>
          <td>1994</td>
          <td>Зелёная миля</td>
          <td>1999</td>
        </tr>
        <tr>
          <th><button className='th-button'><img src={Edit} alt="" /></button></th>
          <td>3</td>
          <td>8.6</td>
          <td>Властелин колец: Возвращение Короля</td>
          <td>2003</td>
          <td>Зелёная миля</td>
          <td>1999</td>
        </tr>
      </tbody>
    </table>
  );
}

export default Table;