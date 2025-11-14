import { getCatalogOfWorks } from "./catalogOfWorks";
import { createClient, getClients, getMyClient, updateClient } from "./clients";
import { getUsers } from "./users";

export const api = {
    users: {
        getUsers
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