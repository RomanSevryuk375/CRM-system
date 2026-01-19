import InfiniteScroll from "react-infinite-scroll-component";
import Edit from "../../assets/svg/Edit.svg";
import Sort from "../../assets/svg/Sort.svg";

export const GenericTable = ({
  headText,
  bodyText,
  columns,
  toggleActive,
  toggleForChange,
  nextHandler,
  hasMore,
}) => {
  return (
    <InfiniteScroll
      dataLength={bodyText.length}
      next={nextHandler}
      hasMore={hasMore}
      scrollableTarget="container"
    >
      <div
        id="container"
        className="table-container"
      >
        <table className="tableMarking">
          <thead className="thead">
            <tr>
              <th className="start-th-button"></th>
              {headText.map((item) => (
                <th key={item} className="сolumn-names">
                  <div className="сolumn-elements">
                    <p className="names">{item}</p>
                    <button 
                      className="button-sort"
                      onClick={() => onSort(columns[index])}
                    >
                      <img
                        className="button-sort-img"
                        src={Sort}
                        alt="Сортировать"
                      />
                    </button>
                  </div>
                </th>
              ))}
            </tr>
          </thead>
          <tbody>
            {bodyText.map((row, index) => (
              <tr
                key={row.id}
                className={`table-row ${index % 2 === 0 ? "even" : "odd"}`}
                onClick={() => {
                  toggleActive(row.id);
                }}
              >
                <td>
                  <button
                    className={`td-button ${index % 2 === 0 ? "even" : "odd"}`}
                    onClick={() => {toggleForChange(row.id)}}
                  >
                    <img src={Edit} alt="Редактировать" />
                  </button>
                </td>
                {columns.map((columnKey, columnIndex) => (
                  <td key={columnIndex} className="td-sum">
                    {row[columnKey]}
                  </td>
                ))}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </InfiniteScroll>
  );
};