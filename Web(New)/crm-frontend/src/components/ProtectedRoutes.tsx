import {Navigate, Outlet} from 'react-router-dom';
import { useAuthStore } from '../store/useAuthStore';

export const ProtectedRoute = ({ allowedRoles }: { allowedRoles?: number[] }) => {
    const user = useAuthStore((state) => state.user);

    if (!user) return <Navigate to="/login" replace />;

    if (allowedRoles && !allowedRoles.includes(user.roleId)) {
        return <Navigate to="/unauthorized" replace />;
    }

    return <Outlet />;
};