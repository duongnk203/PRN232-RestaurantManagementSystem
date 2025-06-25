import api from "./api";

export const categoryService = {
  getCategories: async () => {
    try {
      const response = await api.get("/Category/GetCategories");
      return response;
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  },
};
