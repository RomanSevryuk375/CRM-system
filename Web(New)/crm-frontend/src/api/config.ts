import axios from "axios";

export const apiClient = axios.create({
    baseURL: 'http://localhost:5066/api/v1',
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
