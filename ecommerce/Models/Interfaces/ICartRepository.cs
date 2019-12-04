using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models.Interfaces
{
    interface ICartRepository
    {
        Cart getCart(string key, bool loggedIn);
    }
}
