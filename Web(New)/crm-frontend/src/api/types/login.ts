export interface LoginRequest {
    login?: string;
    password?: string;
}

export interface LoginResponse {
    token: string;
    roleId: number;
    message: string;
}