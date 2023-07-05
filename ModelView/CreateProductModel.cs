using Product_management.Models;
using System.ComponentModel.DataAnnotations;

namespace Product_management.ModelView
{
    public class CreateProductModel
    {
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
