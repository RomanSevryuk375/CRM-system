import { combineReducers } from "redux";
import { catalogReducer } from "./catalogOfWorksReducer";
import { clientsReducer } from "./clientsReducer";
import { usersReducer } from "./users";
import { billsReducer } from "./billsReducer";
import { carsReducer } from "./carsReducer";
import { expensesReducer } from "./expensesReducer";
import { ordersReducer } from "./ordersReducer";
import { paymentNotesReducer } from "./patmentNotesReducer";
import { repairNotesReducer } from "./repairNotesReducer";
import { specializationsReducer } from "./specializationsReducer";
import { statusesReducer } from "./statusesReducer";
import { suppliersReducer } from "./suppliersReducer";
import { taxesReducer } from "./taxesReducer";
import { usedPartsReducer } from "./usedPartsReducer";
import worksReducer from "./worksReducer";
import { workersReducer } from "./workersReducer";
import workProposalReducer from "./workProposalsReducer";

export const rootReducer = combineReducers({
    bills: billsReducer,
    cars: carsReducer,
    catalogOfWorks: catalogReducer,
    clients: clientsReducer,
    expenses: expensesReducer,
    orders: ordersReducer,
    paymentNotes: paymentNotesReducer, 
    repairNotes: repairNotesReducer,
    specializations: specializationsReducer,
    statuses: statusesReducer,
    suppliers: suppliersReducer,
    taxes: taxesReducer,
    usedParts: usedPartsReducer,
    users: usersReducer,
    workers: workersReducer,
    workProposal: workProposalReducer,
    works: worksReducer,
});