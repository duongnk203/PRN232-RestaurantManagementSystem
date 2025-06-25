import api from "./api";

export const promotionService = {
  //Get all promotions
  getPromotions: async (filters = {}) => {
    try {
      const {
        page = 1,
        limit = 12,
        searchPromotion = null,
        isActive = true,
        promotionTypeID = null,
      } = filters;

      let queryParams = new URLSearchParams();

      queryParams.append("PageNumber", page.toString());
      queryParams.append("PageSize", limit.toString());

      if (searchPromotion)
        queryParams.append("SearchPromotion", searchPromotion);
      if (isActive) queryParams.append("IsActive", isActive);
      if (promotionTypeID)
        queryParams.append("PromotionTypeID", promotionTypeID);

      const response = await api.get(
        `/Promotion/GetPromotions?${queryParams.toString()}`
      );
      return response;
    } catch (error) {
      console.error("Error fetching promotions:", error);
      return {
        isSuccess: false,
        message: error.message || "Failed to fetch promotions",
        data: {
          promotions: [],
          query: filters,
        },
        errors: error.response?.data || error.message,
      };
    }
  },

  createPromotion: async (promotionData) => {
    try {
      const response = await api.post(
        "/Promotion/CreatePromotion",
        promotionData
      );
      return response;
    } catch (error) {
      console.error("Error creating promotion:", error);
      return {
        isSuccess: false,
        message: error.message || "Failed to create promotion",
        data: null,
        errors: error.response?.data || error.message,
      };
    }
  },

  updatePromotion: async (promotionData) => {
    try {
      const response = await api.put(
        "/Promotion/UpdatePromotion",
        promotionData
      );
      return response;
    } catch (error) {
      console.error("Error updating promotion:", error);
      return {
        isSuccess: false,
        message: error.message || "Failed to update promotion",
        data: null,
        errors: error.response?.data || error.message,
      };
    }
  },

  deletePromotion: async (promotionID) => {
    try {
      const response = await api.delete(
        `/Promotion/DeletePromotion/${promotionID}`
      );
      return response;
    } catch (error) {
      console.error("Error deleting promotion:", error);
      return {
        isSuccess: false,
        message: error.message || "Failed to delete promotion",
        data: null,
        errors: error.response?.data || error.message,
      };
    }
  },
};
