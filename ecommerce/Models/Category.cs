using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ecommerce.Models
{
    public class Category
    {

        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "nvarchar(250)")]
        [Required]
        public string Name { get; set; }

        public int Position { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }

    }
}
