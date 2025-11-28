import Up from '../../assets/svg/Vector-2.svg';
import Down from '../../assets/svg/Vector-1.svg';
import Histogram from './Histogram';
import HistogramEfficiency from './HistogramEfficiency';
import Efficiency from './Efficiency';
import PieChart from './PieChart';
import Expenses from './Expenses/Expenses';
import './Main.css'



function Main() {
    return (
        <div className="screen">
            <div className="screen-header">
                <div className="left">
                    <div className="left-header">
                        <div className='profit-before-taxes'>
                            <div className='profit-before-taxes-content'>
                                <h1 className='profit-before-taxes-lable'>Чистая прибыль</h1>
                                <div className='profit-numbers'>
                                    <span className='profit-quantity'>$98 000</span>
                                    <div className='profit-procen-img'>
                                        <img className='profit-img-green' src={Up} alt="" />
                                        <span className='profit-procent'>+0,3%</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className='profit-before-taxes'>
                            <div className='profit-before-taxes-content'>
                                <h1 className='profit-before-taxes-lable'>Прибыль до вычета налогов</h1>
                                <div className='profit-numbers'>
                                    <span className='profit-quantity'>$15 000</span>
                                    <div className='profit-procen-img'>
                                        <img className='profit-img-red' src={Down} alt="" />
                                        <span className='profit-procent-red'>-0,3%</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="left-footer">
                        <div className='profit-histogram-header'>
                            <h1 className='profit-histogram-h1'>Прибыль</h1>
                            <select className='profit-histogram-select'>За год</select>
                        </div>
                        <div className='profit-histogram'>
                            <Histogram />
                        </div>
                    </div>
                </div>
                <div className="right">
                    <div className="right-left">
                        <div className='pie-chart'>
                            <div className='profit-histogram-header'>
                                <h1 className='profit-histogram-h1'>Эффективность</h1>
                                <select className='profit-histogram-select'>За год</select>
                            </div>
                            <PieChart />
                        </div>
                        <div className='profit-before-taxes-right'>
                            <div className='profit-before-taxes-content'>
                                <h1 className='profit-before-taxes-lable'>Количество заказ-нарядов за текущий месяц</h1>
                                <div className='profit-numbers'>
                                    <span className='profit-quantity'>115</span>
                                    <div className='profit-procen-img'>
                                        <img className='profit-img-green' src={Up} alt="" />
                                        <span className='profit-procent'>+0,3%</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div className="right-right">
                       <Expenses />
                    </div>
                </div>
            </div>
            <div className="screen-footer">
                <div className="number-of-completed-work-orders">
                    <div className='profit-histogram-header'>
                        <h1 className='profit-histogram-h1'>Эффективность</h1>
                        <select className='profit-histogram-select'>За год</select>
                        <select className='profit-histogram-select'>За год</select>
                    </div>
                    <Efficiency />
                </div>
                <div className="employee-efficiency-gist">
                    <div className='profit-histogram-header'>
                        <h1 className='profit-histogram-h1'>Количество выполненных заказ-нарядов</h1>
                        <select className='profit-histogram-select'>За год</select>
                    </div>
                    <HistogramEfficiency />
                </div>
            </div>
        </div>
    );
}

export default Main;