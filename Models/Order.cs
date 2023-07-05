using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Schema;

namespace Product_management.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Total { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public ICollection<OrderDetail> OrderDetails { get; set; } 
        public User User { get; set; }
        
    }
}
