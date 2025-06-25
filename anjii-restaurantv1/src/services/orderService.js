import api from './api';

export const orderService = {
  // Get all orders with pagination and filters
  getOrders: async (params) => {
    const { page = 1, limit = 10, status, startDate, endDate, staffId } = params;
    let url = `/orders?page=${page}&limit=${limit}`;
    if (status) url += `&status=${status}`;
    if (startDate) url += `&startDate=${startDate}`;
    if (endDate) url += `&endDate=${endDate}`;
    if (staffId) url += `&staffId=${staffId}`;
    return api.get(url);
  },

  // Get order by ID
  getOrderById: async (id) => {
    return api.get(`/orders/${id}`);
  },

  // Create new order
  createOrder: async (orderData) => {
    return api.post('/orders', orderData);
  },

  // Update order status
  updateOrderStatus: async (id, status) => {
    return api.patch(`/orders/${id}/status`, { status });
  },

  // Delete order
  deleteOrder: async (id) => {
    return api.delete(`/orders/${id}`);
  },

  // Get orders by staff
  getOrdersByStaff: async (staffId, params = {}) => {
    const { page = 1, limit = 10, status, startDate, endDate } = params;
    let url = `/orders/staff/${staffId}?page=${page}&limit=${limit}`;
    if (status) url += `&status=${status}`;
    if (startDate) url += `&startDate=${startDate}`;
    if (endDate) url += `&endDate=${endDate}`;
    return api.get(url);
  },

  // Get staff shifts
  getStaffShifts: async (params = {}) => {
    const { page = 1, limit = 10, startDate, endDate, staffId } = params;
    let url = `/shifts?page=${page}&limit=${limit}`;
    if (startDate) url += `&startDate=${startDate}`;
    if (endDate) url += `&endDate=${endDate}`;
    if (staffId) url += `&staffId=${staffId}`;
    return api.get(url);
  },

  // Create staff shift
  createStaffShift: async (shiftData) => {
    return api.post('/shifts', shiftData);
  },

  // Update staff shift
  updateStaffShift: async (id, shiftData) => {
    return api.put(`/shifts/${id}`, shiftData);
  },

  // Delete staff shift
  deleteStaffShift: async (id) => {
    return api.delete(`/shifts/${id}`);
  },
}; 