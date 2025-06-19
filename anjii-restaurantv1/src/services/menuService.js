import api from "./api";

export const menuService = {
  // Get all menu items with pagination and filters
  getMenuItems: async (filters = {}) => {
    try {
      const {
        page = 1,
        limit = 10,
        searchItem = null,
        categoryId = null,
        isAvailable = null,
      } = filters;

      let queryParams = new URLSearchParams();
      queryParams.append("PageNumber", page.toString());
      queryParams.append("PageSize", limit.toString());

      if (searchItem) queryParams.append("SearchItem", searchItem);
      if (categoryId) queryParams.append("CategoryId", categoryId);
      if (isAvailable !== null)
        queryParams.append("IsAvailable", isAvailable.toString());

      console.log(
        "API Request URL:",
        `/Menu/GetMenus?${queryParams.toString()}`
      );
      const response = await api.get(
        `/Menu/GetMenus?${queryParams.toString()}`
      );
      return response;
    } catch (error) {
      console.error("Error fetching menu items:", error);
      return {
        isSuccess: false,
        message: error.message || "Failed to fetch menu items",
        data: {
          menus: [],
          query: filters,
        },
        errors: error.response?.data?.errors || [],
      };
    }
  },

  // Get menu item by ID
  getMenuItemById: async (id) => {
    return api.get(`/menu/${id}`);
  },

  // Create new menu item
  createMenuItem: async (itemData) => {
    try {
      const response = await api.post("/Menu/CreateMenuItem", itemData);
      return response;
    } catch (error) {
      return {
        isSuccess: false,
        message: error.message,
        data: null,
        errors: [error.message],
      };
    }
  },

  // Update menu item
  updateMenuItem: async (itemData) => {
    try {
      const response = await api.put(`/Menu/UpdateMenuItem`, itemData);
      return response;
    } catch (error) {
      return {
        isSuccess: false,
        message: error.message,
        data: null,
        errors: [error.message],
      };
    }
  },

  // Delete menu item
  deleteMenuItem: async (id) => {
    try {
      const response = await api.delete(`/Menu/DeleteMenuItem/${id}`);
      return response;
    } catch (error) {
      return {
        isSuccess: false,
        message: error.message,
        data: null,
        errors: [error.message],
      };
    }
  },

  // Get menu items by category
  getMenuItemsByCategory: async (category) => {
    return api.get(`/menu/category/${category}`);
  },

  // Upload menu item image
  uploadImage: async (file) => {
    const formData = new FormData();
    formData.append("image", file);
    return api.post("/upload", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
  },
};
