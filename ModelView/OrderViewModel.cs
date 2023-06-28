using Product_management.Models;

namespace Product_management.ModelView
{
    public class OrderViewModel
    {
        //  public int UserId { get; set; }
        // public List<Product> ProductIds { get; set; }
        public Guid ser3 { get; set; }
        public Guid ser2 { get; set; }
        public Guid ser1 { get; set; }
       public List<Order> orders { get; set; }
       public Order HighestOrder { get; set; }
       public int number { get; set; }



    }
}
