using Product_management.Models;

namespace Product_management.ModelView
{
    public class OrderProductViewModel
    {
        public OrderViewModel Order { get; set; }
        public List<Product> Product { get; set; }
    }
}
