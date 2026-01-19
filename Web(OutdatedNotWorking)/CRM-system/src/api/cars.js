import { makeRequest } from "./makeRequest";

const URL = "/Car"

export const getCarsWithInfo = (config) => makeRequest({
    method: "GET",
    url: `${URL}/All-with-owner`,
    ...config,
    withCredentials: true,
});

export const getMyCars = (config) => makeRequest({
    method: "GET",
    url: `${URL}/My`,
    ...config,
    withCredentials: true,
    withCredentials: true,
});

export const getInWorkCars = (config) => makeRequest({
    method: "GET",
    url: `${URL}/InWork`,
    ...config,
    withCredentials: true,
});

export const createCar = (data) => makeRequest({
    method: "POST",
    url: URL,
    data,
    withCredentials: true,
});

export const updateCar = (id, data) => makeRequest({
    method: "PUT",
    url: `${URL}/${id}`,
    data,
    withCredentials: true,
});

export const deleteCar = (id) => makeRequest({
    method: "DELETE",
    url: `${URL}/${id}`,
    withCredentials: true,    
});