namespace Product_management.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }  
        public int ProductId { get; set; }
        public int quantity { get; set; }
        public int UnitPrice { get; set; }
        public int TotalPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }   

    }
}
