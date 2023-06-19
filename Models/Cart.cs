using System.ComponentModel.DataAnnotations;

namespace Product_management.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }

        [Required]
        [Range(1,100)]
        public int quantity { get; set; }
    }
}
