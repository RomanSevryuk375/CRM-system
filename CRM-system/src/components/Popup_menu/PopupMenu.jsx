import { useCallback, useEffect, useState } from "react";
import "./PopupMenu.css";
import { useDispatch, useSelector } from "react-redux";
import { createCar } from "../../redux/Actions/cars";
import { createOrder } from "../../redux/Actions/order";
import { createPaymentNote } from "../../redux/Actions/paymentNotes";
import {
  getBillFields,
  getCarFields,
  getClientFields,
  getExpenseFields,
  getMyJournalFields,
  getMyOrderFields,
  getSpecializationFields,
  getSupplierFields,
  getTaxFields,
  getUsedPartFields,
  getWorkerFields,
  getWorkFields,
  getWorkProposalFields,
  getWorkTypeFields,
} from "./fieldConfigs";
import {
  FOOTER_CONTENT_SAVE,
  MODAL_TITLE_BILL,
  MODAL_TITLE_CAR,
  MODAL_TITLE_CLIENT,
  MODAL_TITLE_EXPENSE,
  MODAL_TITLE_ORDER,
  MODAL_TITLE_PAYMENT_NOTE,
  MODAL_TITLE_SPECIALIZATION,
  MODAL_TITLE_SUPPLIER,
  MODAL_TITLE_TAX,
  MODAL_TITLE_USED_PART,
  MODAL_TITLE_WORK,
  MODAL_TITLE_WORK_PROPOSAL,
  MODAL_TITLE_WORK_TYPE,
  MODAL_TITLE_WORKER,
  TOOLBAR_CONTENT_BILL,
  TOOLBAR_CONTENT_CAR,
  TOOLBAR_CONTENT_CLIENT,
  TOOLBAR_CONTENT_EXPENSE,
  TOOLBAR_CONTENT_ORDER,
  TOOLBAR_CONTENT_PAYMENT_NOTE,
  TOOLBAR_CONTENT_SPECIALIZATION,
  TOOLBAR_CONTENT_SUPPLIER,
  TOOLBAR_CONTENT_TAX,
  TOOLBAR_CONTENT_USED_PART,
  TOOLBAR_CONTENT_WORK,
  TOOLBAR_CONTENT_WORK_PROPOSAL,
  TOOLBAR_CONTENT_WORK_TYPE,
  TOOLBAR_CONTENT_WORKER,
} from "./labels";
import { GenericPopupMenu } from "./generic";
import { getInitialFormState } from "./forms";
import { createBill } from "../../redux/Actions/bills";
import { createClient } from "../../redux/Actions/clients";
import { createExpense } from "../../redux/Actions/expenses";
import { createSpecialization } from "../../redux/Actions/specialization";
import { createSupplier } from "../../redux/Actions/suppliers";
import { createTax } from "../../redux/Actions/taxes";
import { createUsedPart } from "../../redux/Actions/usedParts";
import { createWork } from "../../redux/Actions/works";
import { createWorker } from "../../redux/Actions/workers";
import { createWorkProposal } from "../../redux/Actions/workProposals";

function PopupMenu({ isOpen, onClose, activeTable, setPage }) {
  const dispatch = useDispatch();
  const clientId = useSelector((state) => state.clients.myClient?.[0]?.id);
  const workerId = useSelector((state) => state.workers.nyWorker?.[0]?.id);

  const allCars = useSelector((state) => state.cars.cars);
  const allBills = useSelector((state) => state.bills.bills);
  const allOrders = useSelector((state) => state.orders.orders);
  const allStatuses = useSelector((state) => state.statuses.statuses);
  const allTaxes = useSelector((state) => state.taxes.taxes);
  const allUsedParts = useSelector((state) => state.usedParts.usedParts);
  const allSuppliers = useSelector((state) => state.suppliers.suppliers);
  const allWorkers = useSelector((state) => state.workers.workers);
  const allWorks = useSelector((state) => state.works.works);
  const allSpecializations = useSelector(
    (state) => state.specializations.specializations
  );
  

  const [categoryMenu, setCategoryMenu] = useState("car");
  const [options, seOptions] = useState({
    cars: [],
    bills: [],
    orders: [],
    statuses: [],
    taxes: [],
    usedParts: [],
    suppliers: [],
    workers: [],
    works: [],
    specializations: [],
  });
  const [formData, setFormData] = useState({});

  // ... (useEffect для загрузки данных и обновления dynamicOptions, если необходимо)
  // Пример:
  useEffect(() => {
    if (isOpen && (activeTable === "ordersClient") ||  (activeTable === "orders")) {
      // Загрузите список автомобилей и обновите setDynamicOptions({ ...prev, cars: fetchedCars })
      // statuses
      // cars p
    }
    if (isOpen && activeTable === "journalClient") {

    }
    if (isOpen && activeTable === "workers") {
      //specializations
    }
    if (isOpen && activeTable === "works") {
      //orders p
      //workers
      //sattuses
    }
    if (isOpen && activeTable === "parts") {
      //orders p
      //suppliers p
    }
    if (isOpen && activeTable === "bills") {
      //orders p
      //statuses //мб сделать просто перечислением
    }
    if (isOpen && activeTable === "expenses") {
      //taxes
      //usedParts p
    }
  }, [isOpen, activeTable]);

  const handleInputChange = useCallback((e) => {
    const { name, value, type } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: type === "number" ? Number(value) : value,
    }));
  }, []);

  useEffect(() => {
    if (isOpen) {
      let formTypeToInitialize;
      switch (activeTable) {
        case "mainClient":
          formTypeToInitialize = "car";
          break;
        case "ordersClient":
          formTypeToInitialize = "order";
          break;
        case "journalClient":
          formTypeToInitialize = "paymentNote";
          break;
        case "orders":
          formTypeToInitialize = "order";
          break;
        case "clients":
          formTypeToInitialize = "client";
          break;
        case "workers":
          formTypeToInitialize = "worker";
          break;
        case "works":
          formTypeToInitialize = "work";
          break;
        case "parts":
          formTypeToInitialize = "usedPart";
          break;
        case "bills":
          formTypeToInitialize = "bill";
          break;
        case "journal":
          formTypeToInitialize = "paymentNote";
          break;
        case "taxes":
          formTypeToInitialize = "tax";
          break;
        case "expenses":
          formTypeToInitialize = "expense";
          break;
        default:
          formTypeToInitialize = "";
      }
      if (formTypeToInitialize) {
        setFormData(
          getInitialFormState(formTypeToInitialize, clientId, workerId)
        ); //workerId
      }
      setCategoryMenu(formTypeToInitialize);
    }
  }, [isOpen, activeTable]);

  const submitBillForm = (e) => {
    e.preventDefault();
    dispatch(createBill(formData));
    onClose();
    setPage(1);
  };
  const submitCarForm = (e) => {
    e.preventDefault();
    dispatch(createCar(formData));
    // dispatch(getMyCars(1))
    onClose();
    setPage(1);
  };
  const submitClientForm = (e) => {
    e.preventDefault();
    dispatch(createClient(formData));
    onClose();
    setPage(1);
  };
  const submitExpensesForm = (e) => {
    e.preventDefault();
    dispatch(createExpense(formData));
    onClose();
    setPage(1);
  };
  const submitOrderForm = (e) => {
    e.preventDefault();
    dispatch(createOrder(formData));
    onClose();
    setPage(1);
  };
  const submitPaymentNoteForm = (e) => {
    e.preventDefault();
    const fixedData = {
      ...formData,
      date: new Date(formData.date).toISOString(),
    };
    dispatch(createPaymentNote(fixedData));
    onClose();
    setPage(1);
  };
  const submitSpecializationForm = (e) => {
    e.preventDefault();
    dispatch(createSpecialization(formData));
    onClose();
    setPage(1);
  };
  const submitSupplierForm = (e) => {
    e.preventDefault();
    dispatch(createSupplier(formData));
    onClose();
    setPage(1);
  };
  const submitTaxForm = (e) => {
    e.preventDefault();
    dispatch(createTax(formData));
    onClose();
    setPage(1);
  };
  const submitUsedPartForm = (e) => {
    e.preventDefault();
    dispatch(createUsedPart(formData));
    onClose();
    setPage(1);
  };
  const submitWorkForm = (e) => {
    e.preventDefault();
    dispatch(createWork(formData));
    onClose();
    setPage(1);
  };
  const submitWorkerForm = (e) => {
    e.preventDefault();
    dispatch(createWorker(formData));
    onClose();
    setPage(1);
  };
  const submitWorkProposalForm = (e) => {
    e.preventDefault();
    dispatch(createWorkProposal(formData));
    onClose();
    setPage(1);
  };
  const submitWorkTypeForm = (e) => {
    e.preventDefault();
    dispatch(createWorkProposal(formData));
    onClose();
    setPage(1);
  };

  let currentFields = [];
  let currentHandleSubmit = () => {};
  let currentModalTitle = "";
  let currentToolbarContent = [];

  switch (activeTable) {
    case "mainClient":
      currentFields = getCarFields();
      currentHandleSubmit = submitCarForm;
      currentModalTitle = MODAL_TITLE_CAR;
      currentToolbarContent = TOOLBAR_CONTENT_CAR;
      break;
    case "ordersClient":
      currentFields = getMyOrderFields(options);
      currentHandleSubmit = submitOrderForm;
      currentModalTitle = MODAL_TITLE_ORDER;
      currentToolbarContent = TOOLBAR_CONTENT_ORDER;
      break;
    case "journalClient":
      currentFields = getMyJournalFields(options);
      currentHandleSubmit = submitPaymentNoteForm;
      currentModalTitle = MODAL_TITLE_PAYMENT_NOTE;
      currentToolbarContent = TOOLBAR_CONTENT_PAYMENT_NOTE;
      break;
    case "orders":
      currentFields = getMyOrderFields(options);
      currentHandleSubmit = submitOrderForm;
      currentModalTitle = MODAL_TITLE_ORDER;
      currentToolbarContent = TOOLBAR_CONTENT_ORDER;
      break;
    case "clients":
      currentFields = getClientFields();
      currentHandleSubmit = submitClientForm;
      currentModalTitle = MODAL_TITLE_CLIENT;
      currentToolbarContent = TOOLBAR_CONTENT_CLIENT;
      break;
    case "wokers":
      currentFields = getWorkerFields(options);
      currentHandleSubmit = submitWorkerForm;
      currentModalTitle = MODAL_TITLE_WORKER;
      currentToolbarContent = TOOLBAR_CONTENT_WORKER;
      break;
    case "works":
      currentFields = getWorkFields(options);
      currentHandleSubmit = submitWorkForm;
      currentModalTitle = MODAL_TITLE_WORK;
      currentToolbarContent = TOOLBAR_CONTENT_WORK;
      break;
    case "parts":
      currentFields = getUsedPartFields(options);
      currentHandleSubmit = submitUsedPartForm;
      currentModalTitle = MODAL_TITLE_USED_PART;
      currentToolbarContent = TOOLBAR_CONTENT_USED_PART;
      break;
    case "bills":
      currentFields = getBillFields(options);
      currentHandleSubmit = submitBillForm;
      currentModalTitle = MODAL_TITLE_BILL;
      currentToolbarContent = TOOLBAR_CONTENT_BILL;
      break;
    case "journal":
      currentFields = getMyJournalFields(options);
      currentHandleSubmit = submitPaymentNoteForm;
      currentModalTitle = MODAL_TITLE_PAYMENT_NOTE;
      currentToolbarContent = TOOLBAR_CONTENT_PAYMENT_NOTE;
      break;
    case "taxes":
      currentFields = getTaxFields();
      currentHandleSubmit = submitTaxForm;
      currentModalTitle = MODAL_TITLE_TAX;
      currentToolbarContent = TOOLBAR_CONTENT_TAX;
      break;
    case "taxes":
      currentFields = getExpenseFields(options);
      currentHandleSubmit = submitExpensesForm;
      currentModalTitle = MODAL_TITLE_EXPENSE;
      currentToolbarContent = TOOLBAR_CONTENT_EXPENSE;
      break;
    case "specializations":
      currentFields = getSpecializationFields();
      currentHandleSubmit = submitSpecializationForm;
      currentModalTitle = MODAL_TITLE_SPECIALIZATION;
      currentToolbarContent = TOOLBAR_CONTENT_SPECIALIZATION;
      break;
    case "suppliers":
      currentFields = getSupplierFields();
      currentHandleSubmit = submitSupplierForm;
      currentModalTitle = MODAL_TITLE_SUPPLIER;
      currentToolbarContent = TOOLBAR_CONTENT_SUPPLIER;
      break;
    case "propossals":
      currentFields = getWorkProposalFields(options);
      currentHandleSubmit = submitWorkProposalForm;
      currentModalTitle = MODAL_TITLE_WORK_PROPOSAL;
      currentToolbarContent = TOOLBAR_CONTENT_WORK_PROPOSAL;
      break;
    case "workTypes":
      currentFields = getWorkTypeFields();
      currentHandleSubmit = submitWorkTypeForm;
      currentModalTitle = MODAL_TITLE_WORK_TYPE;
      currentToolbarContent = TOOLBAR_CONTENT_WORK_TYPE;
      break;
    default:
      return null;
  }

  return (
    <GenericPopupMenu
      modalTitle={currentModalTitle}
      toolBarContent={currentToolbarContent}
      footerContent={FOOTER_CONTENT_SAVE}
      fields={currentFields}
      formData={formData}
      onClose={onClose}
      isOpen={isOpen}
      setCategoryMenu={setCategoryMenu}
      handleInputChange={handleInputChange}
      handleSubmit={currentHandleSubmit}
    />
  );
}

export default PopupMenu;
