import Filtre from '../assets/Filter.svg';
import './Toolbar.css'

const breadcrumsText = ['Галвная страница AUTOService', '']

function Toolbar() {
    return (
        <div className='toolbar'>
            <div>
                <input type="text" />
                <button><img src={Filtre} alt="" /></button>
            </div>
            <div>
                <button>Создать отчет</button>
                <button>Создать заказ</button>
            </div>
        </div>
    );
}

export default Toolbar;