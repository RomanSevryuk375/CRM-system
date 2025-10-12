import './ClientFAQ.css'
import FaqData from '../../Data/FAQ.json'
import FAQimg from '../../assets/svg/FAQ.svg'
import Cross from '../../assets/svg/Cross.svg'
import { useState } from 'react';


function ClientFAQ() {
    const [openId, setOpenId] = useState(2);

    return (
        <div className='faq-content'>
            <div className='faq-text'>
                <div>
                    <h1 className='faq-text-h1'>ЧАСТО ЗАДАВАЕМЫЕ <span className='faq-pointer'>ВОПРОСЫ</span></h1>
                    {FaqData.map((item) => (
                        <>
                            <div
                                className='question-container'
                                key={item.id}>
                                <h1
                                    className='faq-text-h6'>
                                    {item.question}
                                </h1>
                                <button
                                    className='show-question'
                                    onClick={() => setOpenId(openId === item.id ? null : item.id)}>
                                    <img
                                        className={`show-question-img ${openId === item.id ? 'rotate' : 'base'}`}
                                        src={Cross}/>
                                </button>
                            </div>
                            <div
                                className={`faq-text-span-container ${openId === item.id ? 'show' : 'hide'}`}>
                                <span
                                    className={`faq-text-span ${openId === item.id ? 'show' : 'hide'}`}>{item.answer}</span>
                            </div>
                        </>
                    ))}
                </div>
            </div>
            <div className='faq-img'>
                <img className='faq-shering' src={FAQimg} />
            </div>
        </div>
    );
}
export default ClientFAQ