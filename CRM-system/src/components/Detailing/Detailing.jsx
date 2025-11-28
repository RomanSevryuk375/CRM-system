import './Detailing.css'

function Detailing({ activeFoolMenu, activeDetailing }) {
    if (!activeDetailing) {
        return null;
    }

    return (
        <div className={`container ${activeFoolMenu ? 'enable' : 'disable'}`}>
            <div className='content-container'>
                <div className='block'>
                    <div className='content'>
                        <span className='lable'>ID счета</span>
                        <span className='text'>19</span>
                    </div>
                    <div className='content'>
                        <span className='lable'>Мастера</span>
                        <span className='text'>Севрюк Петр Юрьевич, Стрелков Григорий Григорьевич</span>
                    </div>
                </div>
                <div className='block'>
                    <div className='content'>
                        <span className='lable'>Запчасти</span>
                        <span className='text'>Колесо, руль, передача</span>
                    </div>
                    <div className='content'>
                        <span className='lable'>Выполненные работы</span>
                        <span className='text'>Замена колеса, обмотка руля, регулировка передач, развал-схождение</span>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default Detailing;