using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ecommerce.Models
{
    public class ApplicationUser: IdentityUser
    {
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
