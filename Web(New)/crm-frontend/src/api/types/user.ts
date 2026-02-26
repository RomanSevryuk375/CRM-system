export interface UserRequest {
    roleId: number;
    login: string;
    password: string;
}

export interface UserResponse {
    id: number;
    role: string;
    roleId: number;
    login: string;
    password: string;
}