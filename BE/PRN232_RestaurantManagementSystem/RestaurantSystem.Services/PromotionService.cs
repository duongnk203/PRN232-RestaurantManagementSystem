using RestaurantSystem.DataAccess;
using RestaurantSystem.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface IPromotionService
    {
        Task<ServiceResult<PromotionDTO>> GetPromotionsAsync(PromotionQueryDTO promotionQuery);
        Task<ServiceResult<int>> CreatePromotionAsync(CreatePromotion createPromotion);
        Task<ServiceResult<int>> UpdatePromotionAsync(UpdatePromotion updatePromotion);
        Task<ServiceResult<int>> DeletePromotionAsync(int promotionID);
    }

    public class PromotionService : IPromotionService
    {
        private readonly PromotionDAO _promotionDAO;
        private readonly PromotionUsageDAO _promotionUsageDAO;

        public PromotionService(PromotionDAO promotionDAO, PromotionUsageDAO promotionUsageDAO)
        {
            _promotionDAO = promotionDAO ?? throw new ArgumentNullException(nameof(promotionDAO));
            _promotionUsageDAO = promotionUsageDAO ?? throw new ArgumentNullException(nameof(promotionUsageDAO));
        }

        public async Task<ServiceResult<PromotionDTO>> GetPromotionsAsync(PromotionQueryDTO promotionQuery)
        {
            try
            {
                var promotions = await _promotionDAO.GetPromotionsAsync();
                if (promotions == null || !promotions.Any())
                    return ServiceResult<PromotionDTO>.NotFound("No promotions found");

                if (promotionQuery != null)
                {
                    promotions = promotions
                        .Where(p =>
                            (!promotionQuery.PromotionTypeId.HasValue || p.PromotionTypeId == promotionQuery.PromotionTypeId)
                            && (string.IsNullOrEmpty(promotionQuery.SearchPromotion) || p.PromotionName.Contains(promotionQuery.SearchPromotion, StringComparison.OrdinalIgnoreCase))
                            && (!promotionQuery.IsActive.HasValue || p.IsActive == promotionQuery.IsActive.Value))
                        .Skip((Math.Max(promotionQuery.Page, 1) - 1) * Math.Max(promotionQuery.Limit, 10))
                        .Take(Math.Max(promotionQuery.Limit, 10))
                        .ToList();
                }

                return ServiceResult<PromotionDTO>.Success(new PromotionDTO { Promotions = promotions, Query = promotionQuery }, "Promotions retrieved successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<PromotionDTO>.Error("An error occurred while retrieving promotion list", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResult<int>> CreatePromotionAsync(CreatePromotion createPromotion)
        {
            try
            {
                // Validate input object
                var inputValidationResult = ValidatePromotionInputObject(createPromotion);
                if (!inputValidationResult.IsSuccess)
                    return inputValidationResult;

                // Validate business rules
                var businessValidationResult = await ValidatePromotionBusinessRules(createPromotion);
                if (!businessValidationResult.IsSuccess)
                    return businessValidationResult;

                // Validate promotion type specific fields
                var fieldValidationResult = ValidatePromotionTypeSpecificFields(createPromotion);
                if (!fieldValidationResult.IsSuccess)
                    return fieldValidationResult;

                // Check for duplicate name and type in time range
                if (await _promotionDAO.IsDuplicateNameAndTypeInTimeRange(
                    createPromotion.PromotionName,
                    createPromotion.PromotionTypeId,
                    createPromotion.StartDate,
                    createPromotion.EndDate))
                {
                    return ServiceResult<int>.Error("Promotion name already exists for this type in the specified time range",
                        new List<string> { "Duplicate promotion name" });
                }

                // Check for promotion overlap if active
                if (createPromotion.IsActive && await _promotionDAO.IsPromotionOverlapAsync(
                    0, // 0 for new promotion
                    createPromotion.PromotionTypeId,
                    createPromotion.StartDate,
                    createPromotion.EndDate))
                {
                    return ServiceResult<int>.Error("Promotion overlaps with another active promotion",
                        new List<string> { "Promotion time range overlaps" });
                }

                int result = await _promotionDAO.CreatePromotion(createPromotion);
                return ServiceResult<int>.Success(result, "Promotion created successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.Error("An error occurred while creating promotion", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResult<int>> UpdatePromotionAsync(UpdatePromotion updatePromotion)
        {
            try
            {
                // Validate promotion ID
                if (updatePromotion.PromotionId <= 0)
                    return ServiceResult<int>.Error("Invalid promotion ID",
                        new List<string> { "Promotion ID must be greater than zero" });

                // Validate input object
                var inputValidationResult = ValidatePromotionInputObject(updatePromotion);
                if (!inputValidationResult.IsSuccess)
                    return inputValidationResult;

                // Check if promotion exists
                var currentPromotion = await _promotionDAO.GetPromotionByIdAsync(updatePromotion.PromotionId);
                if (currentPromotion == null)
                    return ServiceResult<int>.NotFound("Promotion not found");

                // Check if promotion is already used
                if (await _promotionUsageDAO.IsPromotionUsedAsync(updatePromotion.PromotionId))
                    return ServiceResult<int>.Error("Promotion is already used and cannot be updated",
                        new List<string> { "Promotion cannot be updated as it has been used" });

                // Validate business rules
                var businessValidationResult = await ValidatePromotionBusinessRules(updatePromotion);
                if (!businessValidationResult.IsSuccess)
                    return businessValidationResult;

                // Validate promotion type specific fields
                var fieldValidationResult = ValidatePromotionTypeSpecificFields(updatePromotion);
                if (!fieldValidationResult.IsSuccess)
                    return fieldValidationResult;

                // Check for duplicate name if name changed
                if (!currentPromotion.PromotionName.Equals(updatePromotion.PromotionName, StringComparison.OrdinalIgnoreCase) &&
                    await _promotionDAO.IsDuplicateNameAndTypeInTimeRange(
                        updatePromotion.PromotionName,
                        updatePromotion.PromotionTypeId,
                        updatePromotion.StartDate,
                        updatePromotion.EndDate))
                {
                    return ServiceResult<int>.Error("Promotion name already exists for this type in the specified time range",
                        new List<string> { "Duplicate promotion name" });
                }

                // Handle active promotion update
                if (currentPromotion.IsActive)
                {
                    // Check for overlaps with other promotions
                    if (await _promotionDAO.IsPromotionOverlapAsync(
                        updatePromotion.PromotionId,
                        updatePromotion.PromotionTypeId,
                        updatePromotion.StartDate,
                        updatePromotion.EndDate))
                    {
                        return ServiceResult<int>.Error("Promotion overlaps with another active promotion",
                            new List<string> { "Promotion time range overlaps" });
                    }

                    // Delete and recreate for active promotions
                    await _promotionDAO.DeletePromotion(updatePromotion.PromotionId);

                    int result = await _promotionDAO.CreatePromotion(new CreatePromotion
                    {
                        PromotionTypeId = updatePromotion.PromotionTypeId,
                        PromotionName = updatePromotion.PromotionName,
                        Description = updatePromotion.Description,
                        StartDate = updatePromotion.StartDate,
                        EndDate = updatePromotion.EndDate,
                        DiscountValue = updatePromotion.DiscountValue,
                        MaxDiscountAmount = updatePromotion.MaxDiscountAmount,
                        BuyQuantity = updatePromotion.BuyQuantity,
                        GetQuantity = updatePromotion.GetQuantity,
                        IsActive = true
                    });

                    return ServiceResult<int>.Success(result, "Promotion updated successfully (new promotion created)");
                }

                // Handle inactive promotion activation
                if (!currentPromotion.IsActive && updatePromotion.IsActive)
                {
                    if (updatePromotion.StartDate < DateTime.Now)
                    {
                        return ServiceResult<int>.Error("Start date cannot be in the past for activating a promotion",
                            new List<string> { "Invalid start date for activation" });
                    }

                    // Check for overlaps when activating
                    if (await _promotionDAO.IsPromotionOverlapAsync(
                        updatePromotion.PromotionId,
                        updatePromotion.PromotionTypeId,
                        updatePromotion.StartDate,
                        updatePromotion.EndDate))
                    {
                        return ServiceResult<int>.Error("Promotion overlaps with another active promotion",
                            new List<string> { "Promotion time range overlaps" });
                    }
                }

                int updated = await _promotionDAO.UpdatePromotion(updatePromotion);
                return updated == 0
                    ? ServiceResult<int>.Error("Promotion not found or no changes made",
                        new List<string> { "No promotion updated" })
                    : ServiceResult<int>.Success(updated, "Promotion updated successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.Error("An error occurred while updating promotion", new List<string> { ex.Message });
            }
        }

        public async Task<ServiceResult<int>> DeletePromotionAsync(int promotionID)
        {
            try
            {
                if (promotionID <= 0)
                    return ServiceResult<int>.Error("Invalid promotion ID",
                        new List<string> { "Promotion ID must be greater than zero" });

                if (await _promotionUsageDAO.IsPromotionUsedAsync(promotionID))
                    return ServiceResult<int>.Error("Promotion is already used and cannot be deleted",
                        new List<string> { "Promotion cannot be deleted as it has been used" });

                int deleted = await _promotionDAO.DeletePromotion(promotionID);
                return deleted == 0
                    ? ServiceResult<int>.NotFound("Promotion not found or already deleted")
                    : ServiceResult<int>.Success(deleted, "Promotion deleted successfully");
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.Error("An error occurred while deleting promotion", new List<string> { ex.Message });
            }
        }

        #region Private Validation Methods

        /// <summary>
        /// Validates basic input object properties
        /// </summary>
        private ServiceResult<int> ValidatePromotionInputObject(dynamic promotion)
        {
            if (promotion == null)
                return ServiceResult<int>.Error("Promotion data is required",
                    new List<string> { "Promotion object cannot be null" });

            if (string.IsNullOrWhiteSpace(promotion.PromotionName))
                return ServiceResult<int>.Error("Promotion name is required",
                    new List<string> { "Promotion name cannot be empty or whitespace" });

            if (promotion.PromotionName.Length > 200) // Assuming max length
                return ServiceResult<int>.Error("Promotion name is too long",
                    new List<string> { "Promotion name cannot exceed 200 characters" });

            if (string.IsNullOrWhiteSpace(promotion.Description))
                return ServiceResult<int>.Error("Promotion description is required",
                    new List<string> { "Promotion description cannot be empty" });

            if (promotion.Description.Length > 1000) // Assuming max length
                return ServiceResult<int>.Error("Promotion description is too long",
                    new List<string> { "Promotion description cannot exceed 1000 characters" });

            if (promotion.PromotionTypeId <= 0 || promotion.PromotionTypeId > 4)
                return ServiceResult<int>.Error("Invalid promotion type",
                    new List<string> { "Promotion type must be between 1 and 4" });

            return ServiceResult<int>.Success(1);
        }

        /// <summary>
        /// Validates business rules for promotion dates and general constraints
        /// </summary>
        private async Task<ServiceResult<int>> ValidatePromotionBusinessRules(dynamic promotion)
        {
            // Date validation
            if (promotion.StartDate >= promotion.EndDate)
                return ServiceResult<int>.Error("Start date must be before end date",
                    new List<string> { "Invalid date range" });

            if (promotion.EndDate <= DateTime.Now.AddDays(-1))
                return ServiceResult<int>.Error("End date cannot be in the past",
                    new List<string> { "Invalid end date" });

            // Date range validation (e.g., not too far in the future)
            if (promotion.StartDate > DateTime.Now.AddYears(2))
                return ServiceResult<int>.Error("Start date cannot be more than 2 years in the future",
                    new List<string> { "Invalid start date range" });

            if ((promotion.EndDate - promotion.StartDate).TotalDays > 365)
                return ServiceResult<int>.Error("Promotion duration cannot exceed 365 days",
                    new List<string> { "Invalid promotion duration" });

            return ServiceResult<int>.Success(1);
        }

        /// <summary>
        /// Validates promotion type specific fields
        /// </summary>
        private ServiceResult<int> ValidatePromotionTypeSpecificFields(dynamic promotion)
        {
            switch ((int)promotion.PromotionTypeId)
            {
                case 1: // Percentage discount
                    return ValidatePercentageDiscount(promotion);

                case 2: // Fixed amount discount
                    return ValidateFixedAmountDiscount(promotion);

                case 3: // Buy X Get Y
                    return ValidateBuyXGetY(promotion);

                case 4: // Combo promotion
                    return ValidateComboPromotion(promotion);

                default:
                    return ServiceResult<int>.Error("Invalid promotion type",
                        new List<string> { "Promotion type not recognized" });
            }
        }

        private ServiceResult<int> ValidatePercentageDiscount(dynamic promotion)
        {
            if (promotion.DiscountValue == null || promotion.DiscountValue <= 0 || promotion.DiscountValue > 100)
                return ServiceResult<int>.Error("Discount value must be between 0 and 100 for percentage discount",
                    new List<string> { "Invalid discount percentage" });

            if (promotion.MaxDiscountAmount != null && promotion.MaxDiscountAmount <= 0)
                return ServiceResult<int>.Error("Max discount amount must be greater than zero",
                    new List<string> { "Invalid max discount amount" });

            if (promotion.BuyQuantity != null)
                return ServiceResult<int>.Error("Buy quantity is not applicable for percentage discount",
                    new List<string> { "Invalid field for percentage discount" });

            if (promotion.GetQuantity != null)
                return ServiceResult<int>.Error("Get quantity is not applicable for percentage discount",
                    new List<string> { "Invalid field for percentage discount" });

            return ServiceResult<int>.Success(1);
        }

        private ServiceResult<int> ValidateFixedAmountDiscount(dynamic promotion)
        {
            if (promotion.DiscountValue == null || promotion.DiscountValue <= 0)
                return ServiceResult<int>.Error("Discount value must be greater than zero for fixed amount discount",
                    new List<string> { "Invalid discount amount" });

            if (promotion.DiscountValue > 10000000) // Assuming max discount amount
                return ServiceResult<int>.Error("Discount amount is too large",
                    new List<string> { "Discount amount exceeds maximum allowed" });

            if (promotion.BuyQuantity != null)
                return ServiceResult<int>.Error("Buy quantity is not applicable for fixed amount discount",
                    new List<string> { "Invalid field for fixed amount discount" });

            if (promotion.GetQuantity != null)
                return ServiceResult<int>.Error("Get quantity is not applicable for fixed amount discount",
                    new List<string> { "Invalid field for fixed amount discount" });

            if (promotion.MaxDiscountAmount != null)
                return ServiceResult<int>.Error("Max discount amount is not applicable for fixed amount discount",
                    new List<string> { "Invalid field for fixed amount discount" });

            return ServiceResult<int>.Success(1);
        }

        private ServiceResult<int> ValidateBuyXGetY(dynamic promotion)
        {
            if (promotion.BuyQuantity == null || promotion.BuyQuantity <= 0)
                return ServiceResult<int>.Error("Buy quantity must be greater than zero for Buy X Get Y promotion",
                    new List<string> { "Invalid buy quantity" });

            if (promotion.GetQuantity == null || promotion.GetQuantity <= 0)
                return ServiceResult<int>.Error("Get quantity must be greater than zero for Buy X Get Y promotion",
                    new List<string> { "Invalid get quantity" });

            if (promotion.BuyQuantity <= promotion.GetQuantity)
                return ServiceResult<int>.Error("Buy quantity must be greater than get quantity",
                    new List<string> { "Invalid buy/get quantity ratio" });

            if (promotion.BuyQuantity > 100 || promotion.GetQuantity > 100) // Reasonable limits
                return ServiceResult<int>.Error("Buy/Get quantities cannot exceed 100",
                    new List<string> { "Quantity exceeds maximum allowed" });

            if (promotion.DiscountValue != null)
                return ServiceResult<int>.Error("Discount value is not applicable for Buy X Get Y promotion",
                    new List<string> { "Invalid field for Buy X Get Y promotion" });

            if (promotion.MaxDiscountAmount != null)
                return ServiceResult<int>.Error("Max discount amount is not applicable for Buy X Get Y promotion",
                    new List<string> { "Invalid field for Buy X Get Y promotion" });

            return ServiceResult<int>.Success(1);
        }

        private ServiceResult<int> ValidateComboPromotion(dynamic promotion)
        {
            if (promotion.BuyQuantity != null)
                return ServiceResult<int>.Error("Buy quantity must be null for combo promotion",
                    new List<string> { "Invalid field for combo promotion" });

            if (promotion.GetQuantity != null)
                return ServiceResult<int>.Error("Get quantity must be null for combo promotion",
                    new List<string> { "Invalid field for combo promotion" });

            if (promotion.DiscountValue != null)
                return ServiceResult<int>.Error("Discount value must be null for combo promotion",
                    new List<string> { "Invalid field for combo promotion" });

            if (promotion.MaxDiscountAmount != null)
                return ServiceResult<int>.Error("Max discount amount must be null for combo promotion",
                    new List<string> { "Invalid field for combo promotion" });

            return ServiceResult<int>.Success(1);
        }

        #endregion
    }
}