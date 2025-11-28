import './ClientContacts.css'

function ClientContacts() {
    return (
        <>
        <h1 className='contact-label'>Контакты</h1>
        <div className='contacts-content'>
            <div className='contacts-content-text'> 
                <div>
                    <h2 className='block-label'>Организация</h2>
                    <span className='block-text'>ООО “AUTOService”</span>
                </div>
                <div>
                    <h2 className='block-label'>Адрес</h2>
                    <span className='block-text'>220638, г. Минск, <br />пр-т. Дзержинского, д. 102</span>
                </div>
                <div>
                    <h2 className='block-label'>Телефоны</h2>
                    <span className='block-text'>+375 (29) 123-45-67 (A1)<br /></span>
                    <span className='block-text'>+375 (44) 123-45-67 (Life)<br /></span>
                    <span className='block-text'>+375 (33) 123-45-67 (MTC)<br /></span>
                    <span className='block-text'>+375 (1770) 12-3-45 (многоканальный)<br /></span>
                </div>
                <div>
                    <h2 className='block-label'>E-mail</h2>
                    <span className='block-text'>autoservice@gmail.com</span>
                </div>
                <div>
                    <h2 className='block-label'>Время работы</h2>
                    <span className='block-text'>пн.-пт. - с 9:00 до 18:00, без перерыва <br />сб. - с 10:00 до 17:00, без перерыва <br />вс. - выходной</span>
                </div>
                <div>
                    <h2 className='block-label'>Данные организации</h2>
                    <span className='block-text'>ИНН 026601687198<br /></span>
                    <span className='block-text'>ОГРН 322774600628427</span>
                </div>
            </div>
            <div className='map'>
                
            </div>
        </div>
        </>
        
    )
}

export default ClientContacts