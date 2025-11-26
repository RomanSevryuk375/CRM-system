import ModalImg from "../../assets/svg/Cross.svg";

export const GenericPopupMenu = ({
  modalTitle, // название формы
  toolBarContent, // инпутф или селекты
  footerContent, // кнопки внизу
  fields, // Теперь это массив объектов-описаний полей
  formData, // Текущие данные формы
  onClose,
  setCategoryMenu,
  isOpen,
  handleInputChange, // Обработчик изменения инпутов
  handleSubmit, // Обработчик отправки формы
}) => {
  if (!isOpen) {
    return null;
  }

  return (
    <div className="shadow">
      <div className="main-menu">
        <div className="modal-header">
          <h2 className="modal-title">{modalTitle}</h2>
          <button onClick={onClose} className="exit">
            <img className="modal-img" src={ModalImg} alt="Закрыть" />
          </button>
        </div>
        <div className="toolbar">
          {toolBarContent.map((item) => (
            <button
              key={item.value}
              onClick={() => setCategoryMenu(item.value)}
              className="modal-toolbar-button"
            >
              {item.name}
            </button>
          ))}
        </div>
        <form className="modal-body" onSubmit={handleSubmit}>
          {fields.map((field) => (
            <div key={field.name} className="modal-select">
              <label htmlFor={field.name} className="select-lable">
                {field.label}
              </label>
              {field.type === "select" ? (
                <select
                  id={field.name}
                  name={field.name}
                  className={`custom-select`}
                  value={formData[field.name] || ""}
                  onChange={handleInputChange}
                  disabled={field.readOnly}
                >
                  <option value="" disabled>
                    {field.placeholder ||
                      `Выберите ${field.label.toLowerCase()}`}
                  </option>
                  {field.options.map((option) => (
                    <option key={option.value} value={option.value}>
                      {option.label}
                    </option>
                  ))}
                </select>
              ) : field.type === "textarea" ? (
                <textarea
                  id={field.name}
                  name={field.name}
                  className={`custom-select`}
                  value={formData[field.name] || ""}
                  onChange={handleInputChange}
                  placeholder={field.placeholder}
                  readOnly={field.readOnly}
                />
              ) : (
                <input
                  id={field.name}
                  name={field.name}
                  type={field.type}
                  className={`custom-select`}
                  value={formData[field.name] || ""}
                  onChange={handleInputChange}
                  placeholder={field.placeholder}
                  readOnly={field.readOnly}
                  step={field.type === "number" ? "any" : undefined}
                />
              )}
            </div>
          ))}
          <div className="modal-footer">
            {footerContent.map((buttonText) => (
              <button key={buttonText} className="custom-button" type="submit">
                {buttonText}
              </button>
            ))}
          </div>
        </form>
      </div>
    </div>
  );
};
