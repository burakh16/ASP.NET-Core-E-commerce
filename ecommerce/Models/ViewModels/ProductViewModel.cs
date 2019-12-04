using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using Microsoft.AspNetCore.Http;

namespace ecommerce.Models.ViewModels
{
    public class ProductViewModel
    {

        public Guid Id { get; set; }
        public string img { get; set; }
        [Required]
        [MaxLength(250, ErrorMessage = "Title cannot exceed 250 characters")]
        public string Title { get; set; }

        [Required] public string Description { get; set; }

        [Required] public string Code { get; set; }


        public string GroupCode { get; set; }


        public string Brand { get; set; }

        public bool Featured { get; set; }

        public IFormFile Image { get; set; }

        [Required] public double Quantity { get; set; }

        [Required] public double Price { get; set; }

        [Required] public string ProductOptionId { get; set; }

        [Required] public string SubCategoryId { get; set; }
    }
}