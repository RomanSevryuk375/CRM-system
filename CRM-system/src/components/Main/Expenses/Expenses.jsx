import './Expenses.css'

function Expenses() {
    return (
        <>
            <div className='wall-exp'>
                <div className='brick-exp'>
                    <h1 className='lable-exp'>Проценты по налогам</h1>
                    <div className='content-container-exp'>
                        <div className='content-text-exp'>
                            <span className='text-exp'>Обязательное страхование</span>
                            <span className='text-exp'>Подоходный налог</span>
                            <span className='text-exp'>Взнос ФСЗН</span>
                            <span className='text-exp'>Выплаты по ФСЗН</span>
                        </div>
                        <div className='content-numbers-exp'>
                            <span className='text-numbers-exp'>0,6%</span>
                            <span className='text-numbers-exp'>13%</span>
                            <span className='text-numbers-exp'>1%</span>
                            <span className='text-numbers-exp'>34%</span>
                        </div>
                    </div>
                </div>
                <div className='brick-exp'>
                    <h1 className='lable-exp'>Коммунальные платежи</h1>
                    <div className='content-container-exp'>
                        <div className='content-text-exp'>
                            <span className='text-exp'>Водоотведение</span>
                            <span className='text-exp'>Теплоснабжение</span>
                            <span className='text-exp'>Горячее водоснабжение</span>
                            <span className='text-exp'>Холодное водоснабжение</span>
                            <span className='text-exp'>Электричество</span>
                        </div>
                        <div className='content-numbers-exp'>
                            <span className='text-numbers-exp'>97$</span>
                            <span className='text-numbers-exp'>15$</span>
                            <span className='text-numbers-exp'>100$</span>
                            <span className='text-numbers-exp'>8$</span>
                            <span className='text-numbers-exp'>61$</span>
                        </div>
                    </div>
                </div>
                <div className='brick-exp'>
                    <div className='content-container-exp'>
                        <div className='content-text-exp'>
                            <span className='text-exp'>Амортизация</span>
                            <span className='text-exp'>Аренда помещений</span>
                        </div>
                        <div className='content-numbers-exp'>
                            <span className='text-numbers-exp'>4%</span>
                            <span className='text-numbers-exp'>550$</span>
                        </div>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Expenses;