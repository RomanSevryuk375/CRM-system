import { makeRequest } from "./makeRequest";

const URL = "/Car"

export const getCarsWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/All-with-owner`,
    ...config,
});

export const getMyCars = (config) => makeRequest({
    method: "GET",
    url: `${URL}/My`,
    ...config,
});

export const getInWorkCars = (config) => makeRequest({
    method: "GET",
    url: `${URL}/InWork`,
    ...config,
});

export const createCar = (config) => makeRequest({
    method: "POST",
    url: URL,
    ...config,
});

export const updateCar = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
});

export const deleteCar = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`
});