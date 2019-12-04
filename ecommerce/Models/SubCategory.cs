using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class SubCategory
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Name { get; set; }

        public int Position { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
