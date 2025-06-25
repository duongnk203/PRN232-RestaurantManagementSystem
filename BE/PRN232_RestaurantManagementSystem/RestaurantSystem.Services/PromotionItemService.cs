using RestaurantSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.Services
{
    public interface IPromotionItemService
    {
       
    }
    public class PromotionItemService : IPromotionItemService
    {
        private readonly PromotionItemDAO _promotionItemDAO;
        public PromotionItemService(PromotionItemDAO promotionItemDAO)
        {
            _promotionItemDAO = promotionItemDAO;
        }

    }
}
