using System.ComponentModel.DataAnnotations;

namespace Product_management.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }  
        public int ProductId { get; set; }

        [Required]
        [Range(0,100)]
        public int quantity { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int UnitPrice { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public int TotalPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }   

    }
}
