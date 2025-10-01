import './FiltreModal.css'

const inputConten = ['Фамилия','Телефон','Имя','Отчество','Почта'];
const filtreButtons = ['Сбросить фильтр','Фильтровать'];

function FiltreModal() {
    return (
        <div className='filtre-modal-body'>
            <div className='filter-modal-position'>
                <div className='filtre-modal-content'>
                    {inputConten.map((item) => (<div key={item} className='filtre-input-container'><p key={item} className='filtre-input-lable'>{item}</p><input className='filtre-input' key={item} type="search"  id={item} /></div>))}
                </div>
                <div className='filtre-modal-footer'>
                    {filtreButtons.map((item) => (<button key={item} className='modal-footer-button'>{item}</button>))}
                    </div>
            </div>
        </div>
    );
}

export default FiltreModal;

// поправить все опечатки даже в названии файла 
// функционал 
// конфигурации через массив объектов
// добавить placeholder
// мб анимашки загрузки 
// мб сохранение настроек фильтров до сброса вручную
// ошибки валидации номеров и почт 
// мб сброс отдельных полей