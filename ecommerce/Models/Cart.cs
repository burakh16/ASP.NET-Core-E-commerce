using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;

namespace ecommerce.Models
{
    public class Cart
    {
        public Guid Id { get; set; }


        public int Quantity { get; set; }
        public Guid ProductId { get; set; }

        public string UserId { get; set; }
        public virtual Product Product { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}