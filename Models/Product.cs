using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Product_management.Models
{
    public class Product
    {
        
      
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Price { get; set; }      
        public bool? Available { get; set; } = true;
        public HashSet<int>? AddressesIdAvailable { get; set; }
        public int SupplierId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<ProductCategory> ProductCategorys { get; set; }

    }
}
