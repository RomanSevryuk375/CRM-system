import Edit from '../../assets/svg/Edit.svg';
import Sort from '../../assets/svg/Sort.svg';
import FiltreModal from '../FilreModal/FiltreModal';
import './Table.css'

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
const headTextWorks = ['№', 'Название работы', 'Категория', 'Описание', 'Нормативное время'];
const bodyTextWorks = [
  { id: 1, title: 'Замена масла и фильтра', category: 'Обслуживание', description: 'Замена моторного масла и масляного фильтра', standart_time: 0.5 },
  { id: 2, title: 'Замена тормозных колодок (перед)', category: 'Тормозная система', description: 'Замена передних тормозных колодок', standart_time: 1.0 },
  { id: 3, title: 'Замена свечей зажигания', category: 'Двигатель', description: 'Замена комплекта свечей зажигания (4-цилиндровый двигатель)', standart_time: 0.8 },
  { id: 4, title: 'Компьютерная диагностика', category: 'Диагностика', description: 'Считывание и анализ кодов ошибок ЭБУ', standart_time: 0.7 },
  { id: 5, title: 'Шиномонтаж R16', category: 'Шиномонтаж', description: 'Снятие, монтаж, балансировка 4 колес R16', standart_time: 1.5 },
  { id: 6, title: 'Ремонт прокола колеса', category: 'Шиномонтаж', description: 'Устранение прокола шины жгутом', standart_time: 0.3 },
  { id: 7, title: 'Замена воздушного фильтра', category: 'Обслуживание', description: 'Замена фильтрующего элемента воздуховода', standart_time: 0.2 },
  { id: 8, title: 'Замена переднего ступичного подшипника', category: 'Ходовая часть', description: 'Демонтаж/монтаж ступичного подшипника с обеих сторон', standart_time: 2.5 },
  { id: 9, title: 'Сход-развал 3D', category: 'Регулировка', description: 'Регулировка углов установки колес на стенде 3D', standart_time: 1.2 },
  { id: 10, title: 'Замена ремня ГРМ', category: 'Двигатель', description: 'Замена ремня газораспределительного механизма с роликами', standart_time: 4.0 },
  { id: 11, title: 'Замена охлаждающей жидкости', category: 'Обслуживание', description: 'Слив старой и заливка новой охлаждающей жидкости', standart_time: 0.6 },
  { id: 12, title: 'Ремонт генератора', category: 'Электрика', description: 'Снятие, ремонт и установка генератора', standart_time: 3.5 },
  { id: 13, title: 'Замена салонного фильтра', category: 'Обслуживание', description: 'Замена фильтра салона', standart_time: 0.3 },
  { id: 14, title: 'Замена сцепления (МКПП)', category: 'Трансмиссия', description: 'Снятие/установка коробки передач и замена сцепления', standart_time: 6.0 },
  { id: 15, title: 'Замена лампы ближнего света', category: 'Электрика', description: 'Замена одной лампы головного света', standart_time: 0.1 },
  { id: 16, title: 'Антикоррозийная обработка днища', category: 'Кузовной ремонт', description: 'Полная обработка днища антикоррозийным составом', standart_time: 8.0 },
  { id: 17, title: 'Проверка уровня технических жидкостей', category: 'Диагностика', description: 'Проверка всех рабочих жидкостей по уровням', standart_time: 0.2 },
  { id: 18, title: 'Замена аккумулятора', category: 'Электрика', description: 'Демонтаж старого и монтаж нового аккумулятора', standart_time: 0.4 },
  { id: 19, title: 'Замена топливного фильтра', category: 'Топливная система', description: 'Замена топливного фильтра (внешний/под днищем)', standart_time: 0.9 },
  { id: 20, title: 'Ремонт глушителя (сварка)', category: 'Выхлопная система', description: 'Сварочные работы по восстановлению герметичности глушителя', standart_time: 1.5 },
  { id: 21, title: 'Прокачка тормозной системы', category: 'Тормозная система', description: 'Удаление воздуха из тормозной системы', standart_time: 1.0 },
  { id: 22, title: 'Замена рычага подвески', category: 'Ходовая часть', description: 'Замена нижнего переднего рычага подвески', standart_time: 1.8 },
  { id: 23, title: 'Ремонт ЭБУ', category: 'Электрика/Диагностика', description: 'Ремонт электронного блока управления (сложный)', standart_time: 10.0 },
  { id: 24, title: 'Полная покраска детали (бампер)', category: 'Малярные работы', description: 'Подготовка, грунтовка и покраска бампера', standart_time: 5.0 },
  { id: 25, title: 'Замена помпы (водяного насоса)', category: 'Двигатель', description: 'Замена водяного насоса системы охлаждения', standart_time: 3.0 }

];
const headTextParts = ['№', '№ заказ-наряда', 'Поставщик', 'Наименование детали', 'Артикул', 'Количество', 'Цена за еденицу'];
const bodyTextParts = [
  { id: 1, order_id: 1001, supplier: 'AutoParts-M', part_name: 'Фильтр масляный', article: 'OX123D', qantity: 5, unit_price: 450 }, 
  { id: 2, order_id: 1002, supplier: 'DistribTech', part_name: 'Колодки тормозные (комплект, перед)', article: 'DB1838', qantity: 2, unit_price: 3200 }, 
  { id: 3, order_id: 1003, supplier: 'CarSupply', part_name: 'Свеча зажигания', article: 'NGK-BPR6ES', qantity: 8, unit_price: 350 }, 
  { id: 4, order_id: 1004, supplier: 'AutoParts-M', part_name: 'Ремень ГРМ', article: 'CT1028', qantity: 1, unit_price: 1850 }, 
  { id: 5, order_id: 1005, supplier: 'DistribTech', part_name: 'Аккумулятор 60 Ач', article: 'VARTA-060', qantity: 1, unit_price: 6500 }, 
  { id: 6, order_id: 1006, supplier: 'CarSupply', part_name: 'Лампа H7 (ближний свет)', article: 'P43T-12V', qantity: 10, unit_price: 250 }, 
  { id: 7, order_id: 1007, supplier: 'AutoParts-M', part_name: 'Стойка стабилизатора (передняя)', article: 'JTS525', qantity: 4, unit_price: 950 }, 
  { id: 8, order_id: 1008, supplier: 'DistribTech', part_name: 'Жидкость тормозная DOT4 (1 л)', article: 'BF4-1L', qantity: 3, unit_price: 700 }, 
  { id: 9, order_id: 1009, supplier: 'CarSupply', part_name: 'Фильтр воздушный', article: 'ELP3910', qantity: 5, unit_price: 600 }, 
  { id: 10, order_id: 1010, supplier: 'AutoParts-M', part_name: 'Насос водяной (помпа)', article: 'WP0315', qantity: 1, unit_price: 4500 }, 
  { id: 11, order_id: 1011, supplier: 'DistribTech', part_name: 'Салонный фильтр (угольный)', article: 'K1211', qantity: 6, unit_price: 800 }, 
  { id: 12, order_id: 1012, supplier: 'CarSupply', part_name: 'Рычаг подвески (нижний, левый)', article: 'QR3805', qantity: 1, unit_price: 3800 }, 
  { id: 13, order_id: 1013, supplier: 'AutoParts-M', part_name: 'Масло моторное 5W-40 (4 л)', article: '5W40-SYN', qantity: 7, unit_price: 2100 }, 
  { id: 14, order_id: 1014, supplier: 'DistribTech', part_name: 'Датчик кислорода (лямбда-зонд)', article: 'LS4001', qantity: 1, unit_price: 5900 }, 
  { id: 15, order_id: 1015, supplier: 'CarSupply', part_name: 'Антифриз G12+ (концентрат, 1 л)', article: 'G12PLUS-1L', qantity: 4, unit_price: 550 }, 
  { id: 16, order_id: 1016, supplier: 'AutoParts-M', part_name: 'Наконечник рулевой тяги', article: 'TRW-900', qantity: 2, unit_price: 1150 }
];


function Table({ activeTable }) {
  {/* <FiltreModal />   */ }
  switch (activeTable) {
    case 'orders':
      return (
        <>
          <div className='table-container'>
            <table className='tableMarking'>
              <thead className='thead'>
                <tr>
                  <th className='start-th-button'></th>
                  {headTextOrders.map((item) => (
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
                {bodyTextOrders.map((row, index) => (<tr key={row.id} className={`table-row ${index % 2 === 0 ? 'even' : 'odd'}`}>
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
    case 'clients':
      return (
        <>
          <div className='table-container'>
            <table className='tableMarking'>
              <thead className='thead'>
                <tr>
                  <th className='start-th-button'></th>
                  {headTextClients.map((item) => (
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
                {bodyTextClients.map((row, index) => (<tr key={row.id} className={`table-row ${index % 2 === 0 ? 'even' : 'odd'}`}>
                  <td><button className={`td-button ${index % 2 === 0 ? 'even' : 'odd'}`}><img src={Edit} alt="" /></button></td>
                  <td className='td-sum'>{row.id}</td>
                  <td className='td-sum'>{row.surname}</td>
                  <td className='td-sum'>{row.name}</td>
                  <td className='td-sum'>{row.patronymic}</td>
                  <td className='td-sum'>{row.phone_number}</td>
                  <td className='td-sum'>{row.mail}</td>
                  <td className='td-sum'>{row.car}</td>
                </tr>))}
              </tbody>
            </table>
          </div>
        </>
      );
    case 'workers':
      return (
        <>
          <div className='table-container'>
            <table className='tableMarking'>
              <thead className='thead'>
                <tr>
                  <th className='start-th-button'></th>
                  {headTextWorkers.map((item) => (
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
                {bodyTextWorkers.map((row, index) => (<tr key={row.id} className={`table-row ${index % 2 === 0 ? 'even' : 'odd'}`}>
                  <td><button className={`td-button ${index % 2 === 0 ? 'even' : 'odd'}`}><img src={Edit} alt="" /></button></td>
                  <td className='td-sum'>{row.id}</td>
                  <td className='td-sum'>{row.surname}</td>
                  <td className='td-sum'>{row.name}</td>
                  <td className='td-sum'>{row.patronymic}</td>
                  <td className='td-sum'>{row.specialization}</td>
                  <td className='td-sum'>{row.hourly_rate}</td>
                  <td className='td-sum'>{row.phone_number}</td>
                </tr>))}
              </tbody>
            </table>
          </div>
        </>
      );
    case 'works':
      return (
        <>
          <div className='table-container'>
            <table className='tableMarking'>
              <thead className='thead'>
                <tr>
                  <th className='start-th-button'></th>
                  {headTextWorks.map((item) => (
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
                {bodyTextWorks.map((row, index) => (<tr key={row.id} className={`table-row ${index % 2 === 0 ? 'even' : 'odd'}`}>
                  <td><button className={`td-button ${index % 2 === 0 ? 'even' : 'odd'}`}><img src={Edit} alt="" /></button></td>
                  <td className='td-sum'>{row.id}</td>
                  <td className='td-sum'>{row.title}</td>
                  <td className='td-sum'>{row.category}</td>
                  <td className='td-sum'>{row.description}</td>
                  <td className='td-sum'>{row.standart_time}</td>
                </tr>))}
              </tbody>
            </table>
          </div>
        </>
      );
    case 'parts':
      return (
        <>
          <div className='table-container'>
            <table className='tableMarking'>
              <thead className='thead'>
                <tr>
                  <th className='start-th-button'></th>
                  {headTextParts.map((item) => (
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
                {bodyTextParts.map((row, index) => (<tr key={row.id} className={`table-row ${index % 2 === 0 ? 'even' : 'odd'}`}>
                  <td><button className={`td-button ${index % 2 === 0 ? 'even' : 'odd'}`}><img src={Edit} alt="" /></button></td>
                  <td className='td-sum'>{row.id}</td>
                  <td className='td-sum'>{row.order_id}</td>
                  <td className='td-sum'>{row.supplier}</td>
                  <td className='td-sum'>{row.part_name}</td>
                  <td className='td-sum'>{row.article}</td>
                  <td className='td-sum'>{row.qantity}</td>
                  <td className='td-sum'>{row.unit_price}</td>
                </tr>))}
              </tbody>
            </table>
          </div>
        </>
      );
  }

}

export default Table;