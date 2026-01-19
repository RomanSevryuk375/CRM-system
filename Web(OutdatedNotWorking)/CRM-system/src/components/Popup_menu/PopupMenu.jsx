import { useCallback, useEffect, useState } from "react";
import "./PopupMenu.css";
import { useDispatch, useSelector } from "react-redux";
import {
  createCar,
  getCarsWithInfo,
  getInWorkCars,
  getMyCars,
} from "../../redux/Actions/cars";
import {
  createOrder,
  getOrdersInWork,
  getOrdersWithInfo,
} from "../../redux/Actions/order";
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
import { createBill, getBills, getMyBills } from "../../redux/Actions/bills";
import { createClient } from "../../redux/Actions/clients";
import { createExpense } from "../../redux/Actions/expenses";
import {
  createSpecialization,
  getSpecializations,
} from "../../redux/Actions/specialization";
import { createSupplier, getSuppliers } from "../../redux/Actions/suppliers";
import { createTax, getTaxes } from "../../redux/Actions/taxes";
import { createUsedPart, getUsedPartsWithInfo } from "../../redux/Actions/usedParts";
import { createWork, getWorksWithInfo } from "../../redux/Actions/works";
import { createWorker, getWorkerWithInfo } from "../../redux/Actions/workers";
import { createWorkProposal } from "../../redux/Actions/workProposals";
import { getStatuses } from "../../redux/Actions/statuses";
import { getCatalogOfWorks } from "../../redux/Actions/catalogOfWorks";

function PopupMenu({ isOpen, onClose, activeTable, setPage }) {
  const dispatch = useDispatch();
  const clientId = useSelector((state) => state.clients.myClient?.[0]?.id);
  const workerId = useSelector((state) => state.workers.myWorker?.[0]?.id);

  const allCars = useSelector((state) => state.cars.cars);
  const allMyCars = useSelector((state) => state.cars.myCars);
  const allBills = useSelector((state) => state.bills.bills);
  const allOrders = useSelector((state) => state.orders.orders);
  const allStatuses = useSelector((state) => state.statuses.statuses);
  const allTaxes = useSelector((state) => state.taxes.taxes);
  const allUsedParts = useSelector((state) => state.usedParts.usedPartsWithInfo);
  const allSuppliers = useSelector((state) => state.suppliers.suppliers);
  const allWorkers = useSelector((state) => state.workers.workersWithInfo);
  const allWorks = useSelector((state) => state.works.works);
  const allWorkTypes = useSelector((state) => state.catalogOfWorks.catalogOfWorks)
  const allSpecializations = useSelector(
    (state) => state.specializations.specializations
  );

  const [categoryMenu, setCategoryMenu] = useState("car");
  const [options, setOptions] = useState({
    cars: [],
    bills: [],
    orders: [],
    statuses: [],
    taxes: [],
    usedParts: [],
    suppliers: [],
    workers: [],
    catalogOfWorks: [],
    works: [],
    specializations: [],
  });
  const [formData, setFormData] = useState({});

  useEffect(() => {
    if (
      (isOpen && activeTable === "ordersClient") ||
      (isOpen && activeTable === "orders")
    ) {
      dispatch(getStatuses()).then(() => {
        const statuses = allStatuses || [];
        if (activeTable === "ordersClient") {
          const filteredStatuses = statuses.filter((status) => status.id === 8);

          const mappedStatuses = filteredStatuses.map((status) => ({
            value: status.id,
            label: status.name,
          }));

          setOptions((prev) => ({
            ...prev,
            statuses: mappedStatuses,
          }));
        } else {
          const filteredStatuses = statuses.filter(
            (status) => status.id >= 4 && status.id <= 9
          );

          const mappedStatuses = filteredStatuses.map((status) => ({
            value: status.id,
            label: status.name,
          }));

          setOptions((prev) => ({
            ...prev,
            statuses: mappedStatuses,
          }));
        }
      });
      if (activeTable === "ordersClient") {
        dispatch(getMyCars(1)).then(() => {
          const myCars = allMyCars || [];

          const filtredCars = myCars.filter((car) => car.ownerId === clientId); // позже сделать фильтрацию по тому в работе машина или нет

          const mappedCars = filtredCars.map((car) => ({
            value: car.id,
            label: `${car.brand} (${car.stateNumber})`,
          }));

          setOptions((prev) => ({
            ...prev,
            cars: mappedCars,
          }));
        });
      } else {
        dispatch(getCarsWithInfo(1)).then(() => {
          const cars = allCars || [];

          // позже сделать фильтрацию по тому в работе машина или нет

          const mappedCars = cars.map((car) => ({
            value: car.id,
            label: `${car.brand} (${car.stateNumber})`,
          }));

          setOptions((prev) => ({
            ...prev,
            cars: mappedCars,
          }));
        });
      }
    }
    if (
      (isOpen && activeTable === "journalClient") ||
      (isOpen && activeTable === "journal")
    ) {
      if (activeTable === "journalClient") {
        dispatch(getMyBills(1)).then(() => {
          const bills = allBills || [];

          const mappedBills = bills.map((bill) => ({
            value: bill.id,
            label: `${bill.date} (${bill.amount} BYN)`,
          }));
          setOptions((prev) => ({
            ...prev,
            bills: mappedBills,
          }));
        });
      } else {
        dispatch(getBills(1)).then(() => {
          const bills = allBills || [];

          const filtredBills = bills.filter((bill) => bill.statusId === 1);

          const mappedBills = filtredBills.map((bill) => ({
            value: bill.id,
            label: `${bill.date} (${bill.amount} BYN)`,
          }));

          setOptions((prev) => ({
            ...prev,
            bills: mappedBills,
          }));
        });
      }
    }
    if (isOpen && activeTable === "workers") {
      dispatch(getSpecializations(1)).then(() => {
        const specializations = allSpecializations || [];

        const mappedSpecializations = specializations.map((specialization) => ({
          value: specialization.id,
          label: `${specialization.name}`,
        }));

        setOptions((prev) => ({
          ...prev,
          specializations: mappedSpecializations,
        }));
      });
    }
    if (isOpen && activeTable === "works") {
      dispatch(getOrdersWithInfo(1)).then(() => {
        const orders = allOrders || [];

        const mappedOrders = orders.map((order) => ({
          value: order.id,
          label: order.carInfo,
        }));

        setOptions((prev) => ({
          ...prev,
          orders: mappedOrders,
        }));
      });

      dispatch(getWorkerWithInfo(1)).then(() => {
        const workers = allWorkers || [];

        const mappedWorkers = workers.map((worker) => ({
          value: worker.id,
          label: worker.name,
        }));

        setOptions((prev) => ({
          ...prev,
          workers: mappedWorkers,
        }));
      });

      dispatch(getCatalogOfWorks(1)).then(() => {
        const works = allWorkTypes || [];
        
        const mappedWorks = works.map((work) => ({
          value: work.id,
          label: work.title,
        }));

        setOptions((prev) => ({
          ...prev,
          catalogOfWorks: mappedWorks,
        }));
      });

      dispatch(getStatuses()).then(() => {
        const statuses = allStatuses || [];
        const filteredStatuses = statuses.filter((status) => status.id === 8);

        const mappedStatuses = filteredStatuses.map((status) => ({
          value: status.id,
          label: status.name,
        }));

        setOptions((prev) => ({
          ...prev,
          statuses: mappedStatuses,
        }));
      });
    }
    if (isOpen && activeTable === "parts") {
      dispatch(getOrdersWithInfo(1)).then(() => {
        const orders = allOrders || [];

        const mappedOrders = orders.map((order) => ({
          value: order.id,
          label: order.carInfo,
        }));

        setOptions((prev) => ({
          ...prev,
          orders: mappedOrders,
        }));
      });

      dispatch(getSuppliers(1)).then(() => {
        const suppliers = allSuppliers || [];

        const mappedSuppliers = suppliers.map((supplier) => ({
          value: supplier.id,
          label: supplier.name,
        }));

        setOptions((prev) => ({
          ...prev,
          suppliers: mappedSuppliers,
        }));
      });
    }
    if (isOpen && activeTable === "bills") {
      dispatch(getOrdersWithInfo(1)).then(() => {
        const orders = allOrders || [];

        const mappedOrders = orders.map((order) => ({
          value: order.id,
          label: order.carInfo,
        }));

        setOptions((prev) => ({
          ...prev,
          orders: mappedOrders,
        }));
      });

      dispatch(getStatuses()).then(() => {
        const statuses = allStatuses || [];
        const filteredStatuses = statuses.filter(
          (status) => status.id >= 1 && status.id <= 3
        );

        const mappedStatuses = filteredStatuses.map((status) => ({
          value: status.id,
          label: status.name,
        }));

        setOptions((prev) => ({
          ...prev,
          statuses: mappedStatuses,
        }));
      });
    }
    if (isOpen && activeTable === "expenses") {
      dispatch(getTaxes(1)).then(() => {
        const taxes = allTaxes || [];

        const mappedTaxes = taxes.map((tax) => ({
          value: tax.id,
          label: tax.name,
        }));

        setOptions((prev) => ({
          ...prev,
          taxes: mappedTaxes,
        }));
      });

      dispatch(getUsedPartsWithInfo(1)).then(() => {
        const usedParts = allUsedParts || [];

        const mappedUsedParts = usedParts.map((usedPart) => ({
          value: usedPart.id,
          label: usedPart.name,
        }));

        setOptions((prev) => ({
          ...prev,
          usedParts: mappedUsedParts,
        }));
      });
    }
    if (isOpen && activeTable === "propossals") {
      dispatch(getOrdersWithInfo(1)).then(() => {
        const orders = allOrders || [];

        const mappedOrders = orders.map((order) => ({
          value: order.id,
          label: order.carInfo,
        }));

        setOptions((prev) => ({
          ...prev,
          orders: mappedOrders,
        }));
      });

      dispatch(getCatalogOfWorks(1)).then(() => {
        const works = allWorkTypes || [];
        
        const mappedWorks = works.map((work) => ({
          value: work.id,
          label: work.title,
        }));

        setOptions((prev) => ({
          ...prev,
          catalogOfWorks: mappedWorks,
        }));
      });

      dispatch(getStatuses()).then(() => {
        const statuses = allStatuses || [];
        const filteredStatuses = statuses.filter((status) => status.id === 8);

        const mappedStatuses = filteredStatuses.map((status) => ({
          value: status.id,
          label: status.name,
        }));

        setOptions((prev) => ({
          ...prev,
          statuses: mappedStatuses,
        }));
      });

      dispatch(getWorkerWithInfo(1)).then(() => {
        const workers = allWorkers || [];

        const mappedWorkers = workers.map((worker) => ({
          value: worker.id,
          label: worker.name,
        }));

        setOptions((prev) => ({
          ...prev,
          workers: mappedWorkers,
        }));
      });
    }
    if (isOpen && activeTable === "ordersWorker") {
      dispatch(getStatuses()).then(() => {
        const statuses = allStatuses || [];
        
          const filteredStatuses = statuses.filter((status) => status.id === 8);

          const mappedStatuses = filteredStatuses.map((status) => ({
            value: status.id,
            label: status.name,
          }));

          setOptions((prev) => ({
            ...prev,
            statuses: mappedStatuses,
        }));
      });

      dispatch(getInWorkCars(1)).then(() => {
        const cars = allCars || [];

        const mappedCars = cars.map((car) => ({
          value: car.id,
          label: `${car.brand} ${car.model} (${car.stateNumber})`,
        }));

        setOptions((prev) => ({
          ...prev,
          cars: mappedCars,
        }));
      });
    }
    if (isOpen || activeTable === "partsWorker") {
      dispatch(getOrdersInWork(1)).then(() => {
        const orders = allOrders || [];

        const mappedOrders = orders.map((order) => ({
          value: order.id,
          label: order.carInfo,
        }));

        setOptions((prev) => ({
          ...prev,
          orders: mappedOrders,
        }));
      });

      dispatch(getSuppliers(1)).then(() => {
        const suppliers = allSuppliers || [];

        const mappedSuppliers = suppliers.map((supplier) => ({
          value: supplier.id,
          label: supplier.name,
        }));

        setOptions((prev) => ({
          ...prev,
          suppliers: mappedSuppliers,
        }));
      });
      if (isOpen && activeTable === "proposalWorker") {
        dispatch(getOrdersInWork(1)).then(() => {
        const orders = allOrders || [];

        const mappedOrders = orders.map((order) => ({
          value: order.id,
          label: order.carInfo,
        }));

        setOptions((prev) => ({
          ...prev,
          orders: mappedOrders,
        }));
        dispatch(getCatalogOfWorks(1)).then(() => {
        const works = allWorkTypes || [];
        
        const mappedWorks = works.map((work) => ({
          value: work.id,
          label: work.title,
        }));

        setOptions((prev) => ({
          ...prev,
          catalogOfWorks: mappedWorks,
        }));
      });

      dispatch(getStatuses()).then(() => {
        const statuses = allStatuses || [];
        const filteredStatuses = statuses.filter((status) => status.id === 8);

        const mappedStatuses = filteredStatuses.map((status) => ({
          value: status.id,
          label: status.name,
        }));

        setOptions((prev) => ({
          ...prev,
          statuses: mappedStatuses,
        }));
      });

      dispatch(getWorkerWithInfo(1)).then(() => {
        const workers = allWorkers || [];

        const mappedWorkers = workers.map((worker) => ({
          value: worker.id,
          label: worker.name,
        }));

        setOptions((prev) => ({
          ...prev,
          workers: mappedWorkers,
        }));
      }); // поменять чтоб был чисто тот работник которы вошел в акк  
      });
      }
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
        case "ordersWorker":
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
        case "partsWorker":
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
        case "propossals":
          formTypeToInitialize = "propossals";
          break;
        case "proposalWorker":
          formTypeToInitialize = "propossals";
          break;
        default:
          formTypeToInitialize = "";
      }
      if (formTypeToInitialize) {
        setFormData(
          getInitialFormState(formTypeToInitialize, clientId, workerId)
        );
      }
      setCategoryMenu(formTypeToInitialize);
    }
  }, [isOpen, activeTable]);

  const submitBillForm = (e) => {
    e.preventDefault();
    const fixedData = {
      ...formData,
      date: new Date(formData.date).toISOString(),
    };
    dispatch(createBill(fixedData));
    onClose();
    setPage(1);
  };
  const submitCarForm = (e) => {
    e.preventDefault();
    dispatch(createCar(formData));
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
    const fixedData = {
      ...formData,
      date: new Date(formData.date).toISOString(),
    };
    dispatch(createExpense(fixedData));
    onClose();
    setPage(1);
  };
  const submitOrderForm = (e) => {
    e.preventDefault();
    const fixedData = {
      ...formData,
      date: new Date(formData.date).toISOString(),
    };
    dispatch(createOrder(fixedData));
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
    const fixedData = {
      ...formData,
      date: new Date(formData.date).toISOString(),
    };
    dispatch(createWorkProposal(fixedData));
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
    case "ordersWorker":
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
    case "workers":
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
    case "partsWorker":
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
    case "expenses":
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
    case "proposalWorker":
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
