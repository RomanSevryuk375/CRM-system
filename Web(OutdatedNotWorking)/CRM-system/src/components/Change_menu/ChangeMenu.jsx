import { useDispatch, useSelector } from "react-redux";
import {
  getCarsWithInfo,
  getInWorkCars,
  getMyCars,
  updateCar,
} from "../../redux/Actions/cars";
import {
  getCarFields,
  getCarWorkerFields,
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
  getWorkWorkerFields,
} from "./FieldConfigs";
import {
  MODAL_TITLE_CAR_UPDATE,
  MODAL_TITLE_CLIENT_UPDATE,
  MODAL_TITLE_EXPENSE_UPDATE,
  MODAL_TITLE_ORDER_UPDATE,
  MODAL_TITLE_PAYMENT_NOTE_UPDATE,
  MODAL_TITLE_SPECIALIZATION_UPDATE,
  MODAL_TITLE_SUPPLIER_UPDATE,
  MODAL_TITLE_TAX_UPDATE,
  MODAL_TITLE_USED_PART_UPDATE,
  MODAL_TITLE_WORK_PROPOSAL_UPDATE,
  MODAL_TITLE_WORK_TYPE_UPDATE,
  MODAL_TITLE_WORK_UPDATE,
  MODAL_TITLE_WORKER_UPDATE,
  TOOLBAR_CONTENT_CAR_UPDATE,
  TOOLBAR_CONTENT_CLIENT_UPDATE,
  TOOLBAR_CONTENT_EXPENSE_UPDATE,
  TOOLBAR_CONTENT_ORDER_UPDATE,
  TOOLBAR_CONTENT_PAYMENT_NOTE_UPDATE,
  TOOLBAR_CONTENT_SPECIALIZATION_UPDATE,
  TOOLBAR_CONTENT_SUPPLIER_UPDATE,
  TOOLBAR_CONTENT_TAX_UPDATE,
  TOOLBAR_CONTENT_USED_PART_UPDATE,
  TOOLBAR_CONTENT_WORK_PROPOSAL_UPDATE,
  TOOLBAR_CONTENT_WORK_TYPE_UPDATE,
  TOOLBAR_CONTENT_WORK_UPDATE,
  TOOLBAR_CONTENT_WORKER_UPDATE,
} from "./labels";
import { useCallback, useEffect, useState } from "react";
import {
  getOrdersInWork,
  getOrdersWithInfo,
  updateOrder,
} from "../../redux/Actions/order";
import { updateClient } from "../../redux/Actions/clients";
import { getWorkerWithInfo, updateWorker } from "../../redux/Actions/workers";
import { updateWork } from "../../redux/Actions/works";
import {
  getUsedPartsWithInfo,
  updateUsedPart,
} from "../../redux/Actions/usedParts";
import { updatePaymentNote } from "../../redux/Actions/paymentNotes";
import { getTaxes, updateTax } from "../../redux/Actions/taxes";
import { updateExpense } from "../../redux/Actions/expenses";
import { updateWorkProposal } from "../../redux/Actions/workProposals";
import {
  getCatalogOfWorks,
  updateWorkType,
} from "../../redux/Actions/catalogOfWorks";
import {
  getSpecializations,
  updateSpecialization,
} from "../../redux/Actions/specialization";
import { getSuppliers, updateSupplier } from "../../redux/Actions/suppliers";
import { FOOTER_CONTENT_SAVE } from "../Popup_menu/labels";
import { getBills, getMyBills } from "../../redux/Actions/bills";
import { getStatuses } from "../../redux/Actions/statuses";
import { getInitialFormState } from "./Forms";
import { GenericPopupMenu } from "../Popup_menu/generic";

function ChangeMenu({ isOpen, onClose, activeTable, setPage, IdForChange }) {
  const dispatch = useDispatch();

  const allCars = useSelector((state) => state.cars.cars);
  const allMyCars = useSelector((state) => state.cars.myCars);
  const allBills = useSelector((state) => state.bills.bills);
  const allOrders = useSelector((state) => state.orders.orders);
  const allStatuses = useSelector((state) => state.statuses.statuses);
  const allTaxes = useSelector((state) => state.taxes.taxes);
  const allUsedParts = useSelector(
    (state) => state.usedParts.usedPartsWithInfo
  );
  const allSuppliers = useSelector((state) => state.suppliers.suppliers);
  const allWorkers = useSelector((state) => state.workers.workersWithInfo);
  const allWorks = useSelector((state) => state.works.works);
  const allWorkTypes = useSelector(
    (state) => state.catalogOfWorks.catalogOfWorks
  );
  const allSpecializations = useSelector(
    (state) => state.specializations.specializations
  );

  const [formData, setFormData] = useState({});
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
            const filteredStatuses = statuses.filter(
              (status) => status.id === 8
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
    if (isOpen && IdForChange) {
      let formTypeToInitialize;
      let selector;
      switch (activeTable) {
        case "mainClient":
          formTypeToInitialize = "car";
          selector = (state) => state.cars.cars.find(car => car.id === IdForChange);
          break;
        case "orders":
          formTypeToInitialize = "order";
          selector = (state) => state.orders.orders.find(order => order.id === IdForChange);
          break;
        case "ordersWorker":
          formTypeToInitialize = "order";
          selector = (state) => state.orders.orders.find(order => order.id === IdForChange);
          break;
        case "journalClient":
          formTypeToInitialize = "paymentNote";
          break;
        case "clients":
          formTypeToInitialize = "client";
          selector = (state) => state.clients.clients.find(client => client.id === IdForChange);
          break;
        case "cars":
          formTypeToInitialize = "car";
          selector = (state) => state.cars.cars.find(car => car.id === IdForChange);
          break;
        case "carsWorker":
          formTypeToInitialize = "carWorker";
          selector = (state) => state.cars.cars.find(car => car.id === IdForChange);
          break;
        case "workers":
          formTypeToInitialize = "worker";
          selector = (state) => state.workers.workersWithInfo.find(worker => worker.id === IdForChange);
          break;
        case "works":
          formTypeToInitialize = "work";
          selector = (state) => state.works.works.find(work => work.id === IdForChange);
          break;
        case "worksWorker":
          formTypeToInitialize = "workWorker";
          selector = (state) => state.works.works.find(work => work.id === IdForChange);
          break;
        case "parts":
          formTypeToInitialize = "usedPart";
          selector = (state) => state.usedParts.usedPartsWithInfo.find(part => part.id === IdForChange);
          break;
        case "partsWorker":
          formTypeToInitialize = "usedPart";
          selector = (state) => state.usedParts.usedPartsWithInfo.find(part => part.id === IdForChange);
          break;
        case "journal":
          formTypeToInitialize = "paymentNote";
          selector = (state => state.paymentNotes.paymentNotes.find(pn => pn.id === IdForChange))
          break;
        case "taxes":
          formTypeToInitialize = "tax";
          selector = (state) => state.taxes.taxes.find(tax => tax.id === IdForChange);
          break;
        case "expenses":
          formTypeToInitialize = "expense";
          selector = (state) => state.expenses.expenses.find(expen => expen.id === IdForChange);
          break;
        case "propossals":
          formTypeToInitialize = "workProposal";
          selector = (state) => state.workProposal.workProposalsWithInfo.find(wp => wp.id === IdForChange);
          break;
        case "proposalWorker":
          formTypeToInitialize = "workProposal";
          selector = (state) => state.workProposal.workProposalsWithInfo.find(wp => wp.id === IdForChange);
          break;
        case "workTypes":
          formTypeToInitialize = "workType";
          selector = (state) => state.catalogOfWorks.catalogOfWorks.find(wt => wt.id === IdForChange);
          break;
        case "specializations":
          formTypeToInitialize = "specialization";
          selector = (state) => state.specializations.specializations.find(spec => spec.id === IdForChange);
          break;
        case "suppliers":
          formTypeToInitialize = "supplier";
          selector = (state) => state.suppliers.suppliers.find(sup => sup.id === IdForChange);
          break;
        default:
          formTypeToInitialize = "";
      }
      if (formTypeToInitialize) {
        setFormData(getInitialFormState(formTypeToInitialize));
      }
    }
  }, [isOpen, activeTable]);

  const submitCarForm = (e) => {
    e.preventDefault();
    dispatch(updateCar(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitOrdersForm = (e) => {
    e.preventDefault();
    dispatch(updateOrder(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitClientsForm = (e) => {
    e.preventDefault();
    dispatch(updateClient(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitWorkersForm = (e) => {
    e.preventDefault();
    dispatch(updateWorker(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitWorksForm = (e) => {
    e.preventDefault();
    dispatch(updateWork(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitPartsForm = (e) => {
    e.preventDefault();
    dispatch(updateUsedPart(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitPaymentsForm = (e) => {
    e.preventDefault();
    dispatch(updatePaymentNote(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitTaxesForm = (e) => {
    e.preventDefault();
    dispatch(updateTax(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitExpensesForm = (e) => {
    e.preventDefault();
    dispatch(updateExpense(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitPropoalsForm = (e) => {
    e.preventDefault();
    dispatch(updateWorkProposal(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitWorkTypeForm = (e) => {
    e.preventDefault();
    dispatch(updateWorkType(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitSpecializationsForm = (e) => {
    e.preventDefault();
    dispatch(updateSpecialization(IdForChange, formData));
    onClose();
    setPage(1);
  };
  const submitSuppliersForm = (e) => {
    e.preventDefault();
    dispatch(updateSupplier(IdForChange, formData));
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
      currentModalTitle = MODAL_TITLE_CAR_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_CAR_UPDATE;
      break;
    case "orders":
      currentFields = getMyOrderFields(options);
      currentHandleSubmit = submitOrdersForm;
      currentModalTitle = MODAL_TITLE_ORDER_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_ORDER_UPDATE;
      break;
    case "ordersWorker":
      currentFields = getMyOrderFields(options);
      currentHandleSubmit = submitOrdersForm;
      currentModalTitle = MODAL_TITLE_ORDER_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_ORDER_UPDATE;
      break;
    case "clients":
      currentFields = getClientFields();
      currentHandleSubmit = submitClientsForm;
      currentModalTitle = MODAL_TITLE_CLIENT_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_CLIENT_UPDATE;
      break;
    case "cars":
      currentFields = getCarFields();
      currentHandleSubmit = submitCarForm;
      currentModalTitle = MODAL_TITLE_CAR_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_CAR_UPDATE;
      break;
    case "carsWorker":
      currentFields = getCarWorkerFields();
      currentHandleSubmit = submitCarForm;
      currentModalTitle = MODAL_TITLE_CAR_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_CAR_UPDATE;
      break;
    case "workers":
      currentFields = getWorkerFields(options);
      currentHandleSubmit = submitWorkersForm;
      currentModalTitle = MODAL_TITLE_WORKER_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_WORKER_UPDATE;
      break;
    case "works":
      currentFields = getWorkFields(options);
      currentHandleSubmit = submitWorksForm;
      currentModalTitle = MODAL_TITLE_WORK_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_WORK_UPDATE;
      break;
    case "worksWorker":
      currentFields = getWorkWorkerFields(options);
      currentHandleSubmit = submitWorksForm;
      currentModalTitle = MODAL_TITLE_WORK_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_WORK_UPDATE;
      break;
    case "parts":
      currentFields = getUsedPartFields(options);
      currentHandleSubmit = submitPartsForm;
      currentModalTitle = MODAL_TITLE_USED_PART_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_USED_PART_UPDATE;
      break;
    case "partsWorker":
      currentFields = getUsedPartFields(options);
      currentHandleSubmit = submitPartsForm;
      currentModalTitle = MODAL_TITLE_USED_PART_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_USED_PART_UPDATE;
      break;
    case "journal":
      currentFields = getMyJournalFields(options);
      currentHandleSubmit = submitPaymentsForm;
      currentModalTitle = MODAL_TITLE_PAYMENT_NOTE_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_PAYMENT_NOTE_UPDATE;
      break;
    case "taxes":
      currentFields = getTaxFields();
      currentHandleSubmit = submitTaxesForm;
      currentModalTitle = MODAL_TITLE_TAX_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_TAX_UPDATE;
      break;
    case "expenses":
      currentFields = getExpenseFields(options);
      currentHandleSubmit = submitExpensesForm;
      currentModalTitle = MODAL_TITLE_EXPENSE_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_EXPENSE_UPDATE;
      break;
    case "propossals":
      currentFields = getWorkProposalFields(options);
      currentHandleSubmit = submitPropoalsForm;
      currentModalTitle = MODAL_TITLE_WORK_PROPOSAL_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_WORK_PROPOSAL_UPDATE;
      break;
    case "proposalWorker":
      currentFields = getWorkProposalFields(options);
      currentHandleSubmit = submitPropoalsForm;
      currentModalTitle = MODAL_TITLE_WORK_PROPOSAL_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_WORK_PROPOSAL_UPDATE;
      break;
    case "workTypes":
      currentFields = getWorkTypeFields();
      currentHandleSubmit = submitWorkTypeForm;
      currentModalTitle = MODAL_TITLE_WORK_TYPE_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_WORK_TYPE_UPDATE;
      break;
    case "specializations":
      currentFields = getSpecializationFields();
      currentHandleSubmit = submitSpecializationsForm;
      currentModalTitle = MODAL_TITLE_SPECIALIZATION_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_SPECIALIZATION_UPDATE;
      break;
    case "suppliers":
      currentFields = getSupplierFields();
      currentHandleSubmit = submitSuppliersForm;
      currentModalTitle = MODAL_TITLE_SUPPLIER_UPDATE;
      currentToolbarContent = TOOLBAR_CONTENT_SUPPLIER_UPDATE;
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
      handleInputChange={handleInputChange}
      handleSubmit={currentHandleSubmit}
    />
  );
}

export default ChangeMenu;
