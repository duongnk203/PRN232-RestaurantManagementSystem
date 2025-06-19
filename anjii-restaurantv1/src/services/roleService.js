import api from './api';

export const roleService = {
    getRoles: async () => {
        try{
            const response = await api.get('/Role/GetRoles');
            return response;
        }catch(error){
            console.error('Error fetching roles:', error);
            return {
                isSuccess: false,
                message: error.message || "Failed to fetch roles",
                data: [],
                errors: [error.message]
            };
        }
    },
}