using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models.ViewModels
{
    public class ProductCategoryViewModel
    {

        public IEnumerable<Product> Products { get; set; }
        public Product Product { get; set; }
        public IEnumerable<Category> Categories { get; set; }

    }
}
