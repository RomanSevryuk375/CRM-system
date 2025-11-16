import { getCatalogOfWorks } from "./catalogOfWorks";
import { createClient, getClients, getMyClient, updateClient } from "./clients";
import { createUser, deleteUser, loginUser, logoutUser } from "./users";

export const api = {
    users: {
        loginUser,
        logoutUser,
        createUser,
        deleteUser,
    },
    catalogOfWorks: {
        getCatalogOfWorks
    },
    clients: {
        getClients,
        getMyClient,
        createClient,
        updateClient,
    }
};