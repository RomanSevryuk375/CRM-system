export interface NotificationRequest {
    clientId: number;
    carId: number;
    typeId: number; // NotificationTypeEnum
    statusId: number; // NotificationStatusEnum
    message: string;
    sendAt: string;
}

export interface NotificationResponse {
    id: number;
    client: string;
    clientId: number;
    car: string;
    carId: number;
    type: string;
    typeId: number;
    status: string;
    statusId: number;
    message: string;
    sendAt: string;
}

export interface NotificationStatusResponse {
    id: number;
    name: string;
}

export interface NotificationTypeResponse {
    id: number;
    name: string;
}

export interface NotificationFilter {
    clientIds?: number[];
    carIds?: number[];
    typeIds?: number[];
    statusIds?: number[];
    sortBy?: string;
    page: number;
    limit: number;
    isDescending: boolean;
}