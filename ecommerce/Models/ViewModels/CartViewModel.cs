using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models.ViewModels
{
    public class CartViewModel
    {
        public int Quantity { get; set; }
      
        public double Price { get; set; }
        public virtual Product Product { get; set; }

    }
}
