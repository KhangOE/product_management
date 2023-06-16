using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;

namespace Product_management.Models
{
    public class Order
    {
        public int Id { get; set; }
        //  public ICollection<Product> Products { get; set; }
        public int UserId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public User User { get; set; }
      //  public int Total { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
