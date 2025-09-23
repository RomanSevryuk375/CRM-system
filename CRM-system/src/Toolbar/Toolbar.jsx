import Filtre from '../assets/Filter.svg';
import './Toolbar.css'


function Toolbar() {
    return (
        <div className='toolbar'>
            <div>
                <input id='1' className='toolbar-input' type="text" />  
                <button className='toolbar-button-filtre'><img src={Filtre}  alt="1" className='toolbar-button-filtre-img'/></button>
            </div>
            <div>
                <button className='toolbar-button'>Создать отчет</button>
                <button className='toolbar-button'>Создать заказ</button>
            </div>
        </div>
    );
}

export default Toolbar;