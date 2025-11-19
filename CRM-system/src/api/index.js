import { getOrdersInWork } from "../redux/Actions/order";
import { createBill, getBills, getMyBills } from "./bills";
import { createCar, deleteCar, getCarsWithInfo, getInWorkCars, getMyCars, updateCar } from "./cars";
import { createWorkType, deleteWorkType, getCatalogOfWorks, updateWorkType } from "./catalogOfWorks";
import { createClient, getClients, getMyClient, updateClient } from "./clients";
import { createExpense, deleteExpense, getExpensesWithInfo, updateExpense } from "./expenses";
import { createOrder, deleteOrder, getOdersWithInfo, getOrdersMy, updateOrder } from "./orders";
import { createPaymentNote, deletePaymentNote, getMyPaymentNotes, getPaymentNotes, updatePaymentNote } from "./paymentNotes";
import { getMyRepairNotes, getRepairNotesInWork, getRepairNotesWithInfo } from "./repairNotes";
import { createSpecialization, deleteSpecialization, getSpecializations, updateSpecialization } from "./specialization";
import { getStatuses } from "./statuses";
import { createSupplier, deleteSupplier, getSuppliers, updateSupplier } from "./suppliers";
import { createTax, deleteTax, getTaxes, updateTax } from "./taxes";
import { createUsedPart, deleteUsedPart, getUsedPartsInWork, getUsedPartsWithInfo, updateUsedPart } from "./usedParts";
import { createUser, deleteUser, loginUser, logoutUser } from "./users";
import { createWorker, deleteWorker, getMyWorker, getWorkerWithInfo, updateWorker } from "./workers";
import { acceptWorkProposal, createWorkProposal, deleteWorkProposal, getWorkProposalInWork, getworkProposalWithInfo, rejectWorkProposal, updateWorkProposal } from "./workProposal";
import { createWork, deleteWork, getMyWorks, getWorksWithInfo, updateWork } from "./works";


export const api = {
    bills: {
        getBills,
        getMyBills,
        createBill,
    },
    cars: {
        getCarsWithInfo,
        getMyCars,
        getInWorkCars,
        createCar,
        updateCar,
        deleteCar,
    },
    catalogOfWorks: {
        getCatalogOfWorks,
        createWorkType,
        updateWorkType,
        deleteWorkType,
    },
    clients: {
        getClients,
        getMyClient,
        createClient,
        updateClient,
    },
    expenses: {
        getExpensesWithInfo,
        createExpense,
        updateExpense,
        deleteExpense,
    },
    orders: {
        getOdersWithInfo,
        getOrdersInWork,
        getOrdersMy,
        createOrder,
        updateOrder,
        deleteOrder,
    },
    paymentNotes: {
        getPaymentNotes,
        getMyPaymentNotes,
        createPaymentNote,
        updatePaymentNote,
        deletePaymentNote,
    },
    repairNotes: {
        getRepairNotesWithInfo,
        getRepairNotesInWork,
        getMyRepairNotes,
    },
    specializations: {
        getSpecializations,
        createSpecialization,
        updateSpecialization,
        deleteSpecialization,
    },
    statuses: {
        getStatuses,
    },
    suppliers: {
        getSuppliers,
        createSupplier,
        updateSupplier,
        deleteSupplier,
    },
    taxes: { 
        getTaxes,
        createTax,
        updateTax,
        deleteTax,
    },
    usedParts: {
        getUsedPartsWithInfo,
        getUsedPartsInWork,
        updateUsedPart,
        createUsedPart,
        deleteUsedPart,
    },
    users: {
        loginUser,
        logoutUser,
        createUser,
        deleteUser,
    },
    works: {
        getMyWorks,
        getWorksWithInfo,
        createWork,
        updateWork,
        deleteWork,
    },
    workers: {
        getWorkerWithInfo,
        getMyWorker,
        createWorker,
        updateWorker,
        deleteWorker,
    }, 
    workProposals: {
        getWorkProposalInWork,
        getworkProposalWithInfo,
        createWorkProposal,
        acceptWorkProposal,
        rejectWorkProposal,
        updateWorkProposal,
        deleteWorkProposal,
    },
};