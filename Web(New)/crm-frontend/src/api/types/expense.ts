export  interface ExpenseResponse {
    id: number;
    date: string;
    category: string;
    taxId?: number;
    partSetId?: number;
    expenseType: string;
    expenseTypeId: number;
    sum: number;
}

export interface ExpenseTypeResponse {
    id: number;
    name: string;
}

export  interface ExpenseRequest {
    date: string;
    category: string;
    taxId?: number;
    partSetId?: number;
    expenseType: string;
    expenseTypeId: number;
    sum: number;
}

export  interface ExpenseUpdateRequest {
    date?: string;
    category?: string;
    expenseTypeId?: number;
    sum?: number;
}

export interface ExpenseFilter {
    taxIds?: number[];
    partSetIds?: number[];
    expenseTypeId?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}