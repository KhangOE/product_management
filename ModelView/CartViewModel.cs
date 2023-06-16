using Product_management.Models;

namespace Product_management.ModelView
{
    public class CartViewModel
    {
        public Product Product { get; set; }
        public int Id { get; set; }
        public string productName { get; set; }
        public int ProductId { get; set; }
        public int price1 { get; set; }
        public int quantity { get; set; }
        public int price { get; set; }
    }
}
