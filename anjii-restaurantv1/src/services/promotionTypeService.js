import api from "./api";

export const promotionTypeService = {
  //Get all promotion types
  getPromotionTypes: async () => {
    try {
      const response = await api.get("/PromotionType/GetPromotionTypes");
      return response;
    } catch (error) {
      console.error("Error fetching promotion types:", error);
      return {
        isSuccess: false,
        message: error.message || "Failed to fetch promotion types",
        data: null,
        errors: error.response?.data || error.message,
      };
    }
  },
};
