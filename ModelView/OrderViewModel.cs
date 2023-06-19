using Product_management.Models;

namespace Product_management.ModelView
{
    public class OrderViewModel
    {
        //  public int UserId { get; set; }
        // public List<Product> ProductIds { get; set; }
       public List<Order> orders { get; set; }
       public Order HighestOrder { get; set; }
       public int number { get; set; }



    }
}
