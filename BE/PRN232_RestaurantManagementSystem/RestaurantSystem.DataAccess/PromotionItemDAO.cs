using Microsoft.EntityFrameworkCore;
using RestaurantSystem.BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantSystem.DataAccess
{
    public class PromotionItemDAO
    {
        private readonly AnJiiDbContext _context;
        public PromotionItemDAO(AnJiiDbContext context)
        {
            _context = context;
        }
                

    }
}
