import { combineReducers } from "redux";
// import { usersReducer } from "./users";
import { catalogReducer } from "./catalogOfWorks";

export const rootReducer = combineReducers({
    // users: usersReducer,
    catalogOfWorks: catalogReducer,
});