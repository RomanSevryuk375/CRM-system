
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import './env.ts'
import {BrowserRouter} from "react-router-dom";
import {QueryClient, QueryClientProvider} from "@tanstack/react-query";
import React from "react";

const queryClient = new QueryClient({
    defaultOptions: {
        queries: {
            refetchOnWindowFocus: false, // Чтобы запросы не летели каждый раз, когда сворачиваешь-разворачиваешь браузер
            retry: 1, // Количество попыток при ошибке сети (по умолчанию 3, для CRM хватает 1-2)
            staleTime: 5 * 60 * 1000, // Данные считаются "свежими" 5 минут, чтобы не дергать бэк лишний раз
        },
    },
});

createRoot(document.getElementById('root')!).render(
    <React.StrictMode>
        <QueryClientProvider client={queryClient}>
            <BrowserRouter>
                <App />
            </BrowserRouter>
            {/* Раскомментируй для дебага (появится кнопка в углу экрана) */}
            {/* <ReactQueryDevtools initialIsOpen={false} /> */}
        </QueryClientProvider>
    </React.StrictMode>
)
