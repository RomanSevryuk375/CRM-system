import axios from "axios";
import { env } from "../env.ts";

export const apiClient = axios.create({
    baseURL: env.VITE_API_BASE_URL,
    withCredentials: true,
});

apiClient.interceptors.response.use(
    (response) => {
        if (response.headers['x-total-count']) {
            return {
                ...response,
                totalCount: parseInt(response.headers['x-total-count'], 10)
            };
        }
        return response;
    },
    (error) => Promise.reject(error)
);

