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
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
