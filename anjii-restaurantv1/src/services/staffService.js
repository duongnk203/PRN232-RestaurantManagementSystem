import api from './api';

export const userService = {
  // Get all users with filters
  getUsers: async (filters = {}) => {
    try {
      // Provide default values for required parameters
      const {
        page = 1,
        limit = 12,
        fullName = null,
        isActive = true,
        roleId = null
      } = filters;

      let queryParams = new URLSearchParams();
      
      // Add required params with their values (converted to strings)
      queryParams.append('PageNumber', page.toString());
      queryParams.append('PageSize', limit.toString());

      // Add optional filters only if they have values
      if (fullName) queryParams.append('FullName', fullName);
      if (roleId) queryParams.append('RoleId', roleId);

      const response = await api.get(`/Staff/GetStaffs?${queryParams.toString()}`);
      
      // API trả về đúng cấu trúc {isSuccess, message, data: {staffs, query}, errors}
      // nên chúng ta có thể trả về trực tiếp
      return response;

    } catch (error) {
      console.error('Error fetching staff:', error);
      return {
        isSuccess: false,
        message: error.message || "Failed to fetch staff list",
        data: {
          staffs: [],
          query: filters
        },
        errors: [error.message]
      };
    }
  },

  // Get user by ID
  getUserById: async (id) => {
    try {
      const response = await api.get(`/staffs/${id}`);
      return response;
    } catch (error) {
      return {
        isSuccess: false,
        message: error.message,
        data: null,
        errors: [error.message]
      };
    }
  },

  // Create new user
  createUser: async (userData) => {
    try {
      const response = await api.post('/Staff/CreateStaff', userData);
      return response;
    } catch (error) {
      return {
        isSuccess: false,
        message: error.message,
        data: null,
        errors: [error.message]
      };
    }
  },

  // Update user
  updateUser: async (userData) => {
    try {
      const response = await api.put(`/Staff/UpdateStaff`, userData);
      return response;
    } catch (error) {
      return {
        isSuccess: false,
        message: error.message,
        data: null,
        errors: [error.message]
      };
    }
  },

  // Delete user
  deleteUser: async (id) => {
    try {
      const response = await api.delete(`/Staff/DeleteStaff/${id}`);
      return response;
    } catch (error) {
      return {
        isSuccess: false,
        message: error.message,
        data: null,
        errors: [error.message]
      };
    }
  },

  // Change user role
  changeUserRole: async (id, role) => {
    try {
      const response = await api.patch(`/staffs/${id}/role`, { role });
      return response;
    } catch (error) {
      return {
        isSuccess: false,
        message: error.message,
        data: null,
        errors: [error.message]
      };
    }
  },
}; 