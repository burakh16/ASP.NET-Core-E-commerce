using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Data;

namespace ecommerce.Models.Services
{
    public class CartRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CartRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Cart Get(string key, bool loggedIn)
        {
            if (loggedIn)
            {
                dbContext.Carts.Where(x => x.UserId == key);
            }
            return new Cart();
        }
    }
}