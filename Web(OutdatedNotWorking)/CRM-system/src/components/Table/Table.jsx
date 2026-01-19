import { useEffect, useRef, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Main from "../Main/Main";
import Detailing from "../Detailing/Detailing";
import CarCard from "../CarCard/CarCard";
import PPFooter from "../PPFooter/PPFooter";
import { GenericTable } from "./generic";
import { getBills, getMyBills } from "../../redux/Actions/bills";
import { getCatalogOfWorks } from "../../redux/Actions/catalogOfWorks";
import { getClients } from "../../redux/Actions/clients";
import { getExpensesWithInwo } from "../../redux/Actions/expenses";
import { getMyOrders, getOrdersInWork, getOrdersWithInfo } from "../../redux/Actions/order";
import {
  getMyPaymentNotes,
  getPaymentNotes,
} from "../../redux/Actions/paymentNotes";
import { getMyRepairNotes, getRepairNotesInWork } from "../../redux/Actions/repairNotes";
import { getTaxes } from "../../redux/Actions/taxes";
import { getUsedPartsInWork, getUsedPartsWithInfo } from "../../redux/Actions/usedParts";
import { getWorkerWithInfo } from "../../redux/Actions/workers";
import "./Table.css";
import { getWorkProposalForCar, getWorkProposalInWork, getWorkProposalWithInfo } from "../../redux/Actions/workProposals";
import { getSpecializations } from "../../redux/Actions/specialization";
import { getSuppliers } from "../../redux/Actions/suppliers";
import { getMyWorks, getWorksWithInfo } from "../../redux/Actions/works";
import {
  columnsBills,
  columnsBillsClient,
  columnsCatalogOfWorks,
  columnsClients,
  columnsExpenses,
  columnsHistoryClient,
  columnsInWorkCars,
  columnsJournal,
  columnsJournalClient,
  columnsOrdersClient,
  columnsOreders,
  columnsParts,
  columnsProposalsForCar,
  columnsSpecializations,
  columnsSuppliers,
  columnsTaxes,
  columnsWorkers,
  columnsWorkProposals,
  columnsWorks,
  columnsWorksForCar,
  headTextBills,
  headTextBillsClient,
  headTextCatalogOftWorks,
  headTextClients,
  headTextExpenses,
  headTextHistoryClient,
  headTextInWorkCars,
  headTextJournal,
  headTextJournalClient,
  headTextOrders,
  headTextOrdersClient,
  headTextParts,
  headTextProposalsForCar,
  headTextSpecializations,
  headTextSuppliers,
  headTextTaxes,
  headTextWorkers,
  headTextWorkProposals,
  headTextWorks,
  headTextWorksForCar,
} from "./configs";
import { getInWorkCars } from "../../redux/Actions/cars";

function Table({ activeTable, isMod, setIsMod, setActiveDetailing, activeDetailing, forChange, setForChange, setPage, page }) {

  const dispatch = useDispatch();
  const isLoadingRef = useRef(false);
  const isSwitchingTable = useRef(false);

  const toggleActiveRow = (rowId) => {
    setActiveDetailing((prev) => (prev === rowId ? null : rowId))
  }

  const toggleForChange = (Id) => {
    setForChange((prev) => (prev === Id ? null : Id))
  }

  useEffect(() => {
    isSwitchingTable.current = true;
    setPage(1);
  }, [activeTable]);

  const catalogOfWorks = useSelector(
    (state) => state.catalogOfWorks.catalogOfWorks
  );
  const orders = useSelector((state) => state.orders.orders);
  const ordersClient = useSelector((state) => state.orders.myOrders);
  const ordersInWork = useSelector((state) => state.orders.inWorkOrders);
  const clients = useSelector((state) => state.clients.clients);
  const workers = useSelector((state) => state.workers.workersWithInfo);
  const specializations = useSelector(
    (state) => state.specializations.specializations
  );
  const supplier = useSelector((state) => state.suppliers.suppliers);
  const works = useSelector((state) => state.works.worksWithInfo);
  const workProposals = useSelector(
    (state) => state.workProposals.workProposalsWithInfo
  );
  const parts = useSelector((state) => state.usedParts.usedPartsWithInfo);
  const partsInWork = useSelector((state) => state.usedParts.usedPartsInWork)
  const bills = useSelector((state) => state.bills.bills);
  const billsClient = useSelector((state) => state.bills.myBills);
  const journal = useSelector((state) => state.paymentNotes.paymentNotes);
  const journalClient = useSelector(
    (state) => state.paymentNotes.myPaymentNotes
  );
  const repairNotesClient = useSelector(
    (state) => state.repairNotes.myRepairNotes
  );
  const repairNotesInWork = useSelector((state) => state.repairNotes.repairNotesInWork);
  const taxes = useSelector((state) => state.taxes.taxes);
  const expenses = useSelector((state) => state.expenses.expenses);
  const workProposalForCar = useSelector((state) => state.workProposals.workProposalsForCar);
  const workProposalInWork = useSelector((state) => state.workProposals.workProposalsInWork);
  const workForCars = useSelector((state) => state.works.worksForCar);
  const workForWorker = useSelector((state) => state.works.myWorks);
  const carsInWork = useSelector((state) => state.cars.inWorkCars);
  
  const totalCatalog = useSelector(
    (state) => state.catalogOfWorks.totalCatalog
  );
  const totalOrders = useSelector((state) => state.orders.totalOrders);
  const totalOrdersClient = useSelector((state) => state.orders.myOrdersTotal);
  const totalOrdersInWork = useSelector((state) => state.orders.inWorkOrdersTotal);
  const totalClients = useSelector((state) => state.clients.totalClients);
  const totalWorkers = useSelector(
    (state) => state.workers.workersWithInfoTotal
  );
  const totalSpecializations = useSelector(
    (state) => state.specializations.specializationsTotal
  );
  const totalSuppliers = useSelector((state) => state.suppliers.totalSuppliers);
  const totalWorks = useSelector((state) => state.works.worksWithInfoTotal);
  const totalWorkForWorker = useSelector((state) => state.works.myWorksTotal);
  const totalWorkProposals = useSelector(
    (state) => state.workProposals.workProposalsWithInfoTotal
  );
  const totalWorkProposalsInWork = useSelector((state) => state.workProposals.workProposalsInWorkTotal);
  const totalParts = useSelector(
    (state) => state.usedParts.usedPartsWithInfoTotal
  );
  const totalPartsInWork = useSelector((state) => state.usedParts.usedPartsInWorkTotal);
  const totalBills = useSelector((state) => state.bills.billsTotal);
  const totalBillsClient = useSelector((state) => state.bills.myBillsTotal);
  const totalJournal = useSelector(
    (state) => state.paymentNotes.paymentNotesTotal
  );
  const totalJournalClient = useSelector(
    (state) => state.paymentNotes.myPaymentNotesTotal
  );
  const totalRepairNotesClient = useSelector(
    (state) => state.repairNotes.myRepairNotesTotal
  );
  const totalRepairNotesInWork = useSelector( 
    (state) => state.repairNotes.repairNotesInWorkTotal
  );
  const totalTaxes = useSelector((state) => state.taxes.totalTaxes);
  const totalExpenses = useSelector((state) => state.expenses.expensesTotal);
  const totalCarsInWork = useSelector((state) => state.cars.inWorkCarsTotal);

  const tableConfig = {
    orders: {
      data: orders,
      total: totalOrders,
      action: getOrdersWithInfo,
      columns: columnsOreders,
      headText: headTextOrders,
      needsDetailing: true,
    },
    ordersClient: {
      data: ordersClient,
      total: totalOrdersClient,
      action: getMyOrders,
      columns: columnsOrdersClient,
      headText: headTextOrdersClient,
      needsDetailing: true,
    },
    ordersWorker: {
      data: ordersInWork, 
      total: totalOrdersInWork,
      action: getOrdersInWork,
      columns: columnsOreders,
      headText: headTextOrders,
      needsDetailing: false,
    },
    clients: {
      data: clients,
      total: totalClients,
      action: getClients,
      columns: columnsClients,
      headText: headTextClients,
      needsDetailing: true,
    },
    carsWorker: {
      data: carsInWork,
      total: totalCarsInWork,
      action: getInWorkCars,
      columns: columnsInWorkCars,
      headText: headTextInWorkCars,
      needsDetailing: true,
    },
    workers: {
      data: workers,
      total: totalWorkers,
      action: getWorkerWithInfo,
      columns: columnsWorkers,
      headText: headTextWorkers,
      needsDetailing: true,
    },
    works: {
      data: works,
      total: totalWorks,
      action: getWorksWithInfo,
      columns: columnsWorks,
      headText: headTextWorks,
      needsDetailing: false,
    },
    parts: {
      data: parts,
      total: totalParts,
      action: getUsedPartsWithInfo,
      columns: columnsParts,
      headText: headTextParts,
      needsDetailing: false,
    },
    partsWorker: {
      data: partsInWork,
      total: totalPartsInWork,
      action: getUsedPartsInWork,
      columns: columnsParts,
      headText: headTextParts,
      needsDetailing: false,
    },
    bills: {
      data: bills,
      total: totalBills,
      action: getBills,
      columns: columnsBills,
      headText: headTextBills,
      needsDetailing: true,
    },
    billsClient: {
      data: billsClient,
      total: totalBillsClient,
      action: getMyBills,
      columns: columnsBillsClient,
      headText: headTextBillsClient,
      needsDetailing: true,
    },
    journal: {
      data: journal,
      total: totalJournal,
      action: getPaymentNotes,
      columns: columnsJournal,
      headText: headTextJournal,
      needsDetailing: false,
    },
    journalClient: {
      data: journal,
      total: totalJournalClient,
      action: getMyPaymentNotes,
      columns: columnsJournal,
      headText: headTextJournal,
      needsDetailing: false,
    },
    historyClient: {
      data: repairNotesClient,
      total: totalRepairNotesClient,
      action: getMyRepairNotes,
      columns: columnsHistoryClient,
      headText: headTextHistoryClient,
      needsDetailing: true,
    },
    historyWorker: {
      data: repairNotesInWork,
      total: totalRepairNotesInWork,
      action: getRepairNotesInWork,
      columns: columnsHistoryClient,
      headText: headTextHistoryClient,
      needsDetailing: true,
    },
    taxes: {
      data: taxes,
      total: totalTaxes,
      action: getTaxes,
      columns: columnsTaxes,
      headText: headTextTaxes,
      needsDetailing: false,
    },
    expenses: {
      data: expenses,
      total: totalExpenses,
      action: getExpensesWithInwo,
      columns: columnsExpenses,
      headText: headTextExpenses,
      needsDetailing: false,
    },
    propossals: {
      data: workProposals,
      total: totalWorkProposals,
      action: getWorkProposalWithInfo,
      columns: columnsWorkProposals,
      headText: headTextWorkProposals,
      needsDetailing: false,
    },
    proposalWorker: {
      data: workProposalInWork,
      total: totalWorkProposalsInWork,
      action: getWorkProposalInWork,
      columns: columnsWorkProposals,
      headText: headTextWorkProposals,
      needsDetailing: false,
    },
    workTypes: {
      data: catalogOfWorks,
      total: totalCatalog,
      action: getCatalogOfWorks,
      columns: columnsCatalogOfWorks,
      headText: headTextCatalogOftWorks,
      needsDetailing: false,
    },
    specializations: {
      data: specializations,
      total: totalSpecializations,
      action: getSpecializations,
      columns: columnsSpecializations,
      headText: headTextSpecializations,
      needsDetailing: false,
    },
    suppliers: {
      data: supplier,
      total: totalSuppliers,
      action: getSuppliers,
      columns: columnsSuppliers,
      headText: headTextSuppliers,
      needsDetailing: false,
    },
    worksForCar: {
      data: workForCars,
      total: 0,
      action: null,
      columns: columnsWorksForCar,
      headText: headTextWorksForCar,
      needsDetailing: false,
    },
    worksWorker: {
      data: workForWorker,
      total: totalWorkForWorker,
      action: getMyWorks,
      columns: columnsWorks,
      headText: headTextWorks,
      needsDetailing: false,
    },
    proposalsForCar: {
      data: workProposalForCar.filter((prop) => prop.statusName !== "Принят"), 
      total: null,
      action: getWorkProposalForCar,
      columns: columnsProposalsForCar,
      headText: headTextProposalsForCar,
      needsDetailing: false,
    },
  };

  useEffect(() => {
    const cfg = tableConfig[activeTable];
    if (!cfg || !cfg.action) return;

    if (isSwitchingTable.current) {
      if (page === 1) {
        isSwitchingTable.current = false;
      } else {
        return;
      }
    }

    if (isLoadingRef.current) return;
    isLoadingRef.current = true;

    dispatch(cfg.action(page)).finally(() => {
      isLoadingRef.current = false;
    });
  }, [page, activeTable, dispatch]);

  const nextHandler = () => {
    if (isLoadingRef.current) return;
    const cfg = tableConfig[activeTable];
    if (!cfg) return;

    const dataLen = (cfg.data && cfg.data.length) || 0;
    const total = cfg.total || 0;
    if (dataLen >= total) return;
    setPage((p) => p + 1);
  };

  const cfg = tableConfig[activeTable];
  const dataForRender = cfg ? cfg.data || [] : [];
  const hasMore = cfg ? dataForRender.length < (cfg.total || 0) : false;

  if (activeTable === "main") return <Main />;

  if (activeTable === "mainClient") {
    return (
      <>
        <CarCard
          isMod={isMod}
          setIsMod={setIsMod}
          page={page}
          setPage={setPage}
        />
        <PPFooter />
      </>
    );
  }
  if (cfg) {
    return (
      <>
        <GenericTable
          headText={cfg.headText}
          bodyText={dataForRender || []}
          columns={cfg.columns}
          toggleActive={toggleActiveRow}
          toggleForChange={toggleForChange}
          nextHandler={cfg.action ? nextHandler : undefined}
          hasMore={cfg.action ? hasMore : false}
        />
        {cfg.needsDetailing && <Detailing activeDetailing={activeDetailing} />}
      </>
    );
  }

  return <h1>Error</h1>;
}

export default Table;
