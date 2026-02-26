export interface CarResponse {
    id: number;
    owner: string;
    ownerId: number;
    status: string;
    statusId: number;
    brand: string;
    model: string;
    yearOfManufacture: number;
    vinNumber: string;
    stateNumber: string;
    mileage: number;
}

export interface CarStatusResponse {
    id: number;
    name: string;
}

export interface CarRequest {
    id: number;
    ownerId: number;
    statusId: number;
    brand: string;
    model: string;
    yearOfManufacture: number;
    vinNumber: string;
    stateNumber: string;
    mileage: number;
}

export interface CarUpdateRequest {
    statusId?: number;
    brand?: string;
    model?: string;
    yearOfManufacture?: number;
    mileage?: number;
}

export interface CarFilter {
    ownerIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}