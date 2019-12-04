using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ecommerce.Models
{
    public class Product
    {
        [Key] public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Slug { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }

        public string GroupCode { get; set; }

        public string Brand { get; set; }

        public bool Featured { get; set; }

        public string Image { get; set; }

        public double Quantity { get; set; }
        public double Price { get; set; }

        public Guid ProductOptionId { get; set; }

        public Guid SubCategoryId { get; set; }


        [ForeignKey("SubCategoryId")] public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}