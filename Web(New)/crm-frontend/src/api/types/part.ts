export interface PartRequest {
    categoryId: number;
    oemArticle?: string;
    manufacturerArticle?: string;
    internalArticle: string;
    description?: string;
    name: string;
    manufacturer: string;
    applicability: string;
}

export interface PartResponse {
    id: number;
    categoryId: number;
    oemArticle?: string;
    manufacturerArticle?: string;
    internalArticle: string;
    description?: string;
    name: string;
    manufacturer: string;
    applicability: string;
}

export interface PartUpdateRequest {
    oemArticle?: string;
    manufacturerArticle?: string;
    internalArticle?: string;
    description?: string;
    name?: string;
    manufacturer?: string;
    applicability?: string;
}

export interface PartFilter {
    categoryIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}