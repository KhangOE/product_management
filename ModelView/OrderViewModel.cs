using Product_management.Models;

namespace Product_management.ModelView
{
    public class OrderViewModel
    {
        //  public int UserId { get; set; }
        // public List<Product> ProductIds { get; set; }
        public DateTime date { get; set; }
        public int id { get; set; }
        public int IdH { get; set; }
        public int Total { get; set; }
        public int UserId { get; set; }
        public int HUserId { get; set; }
        public List<int> ProductName { get; set; }
        public int highestUserTotal { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public List<List<OrderDetail>> Details { get; set; }
    }
}
