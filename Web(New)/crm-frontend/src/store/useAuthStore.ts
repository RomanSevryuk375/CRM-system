import {create} from "zustand";
import {persist} from "zustand/middleware";
import {userService} from "../api/services/userService.ts";

interface User {
    id: number;
    roleId: number;
    profileId: number;
}

interface AuthStore {
    user: User | null;
    setAuth: (user: User) => void;
    logout: () => void;
}

export const useAuthStore = create<AuthStore>()(
    persist(
        (set) => ({
            user: null,
            setAuth: (user) => set({ user }),
            logout: () => {
                set({user: null});
                void userService.logout();
            },
        }),
        { name: 'auth-storage'}
    )
);