import './Navigation.css';

function Navigation({ activeFoolMenu, setActiveTable, navButtonsConfig }) {
    return (
        <>
            <div className={`buttons ${activeFoolMenu ? 'expanded' : 'folded'}`}>
                {navButtonsConfig.map((row, index) => (
                    <button
                        key={index}
                        className={`navigation-button ${activeFoolMenu ? 'expanded' : 'folded'}`}
                        onClick={() => setActiveTable(row.value)}
                    >
                        <img className={`button-img ${activeFoolMenu ? 'expanded' : 'folded'}`}
                            src={row.icon}
                            alt={row.text}
                        />
                        {activeFoolMenu && (
                            <span className='button-text'>{row.text}</span>
                        )}
                    </button>
                ))}
            </div>
        </>
    );
}

export default Navigation;