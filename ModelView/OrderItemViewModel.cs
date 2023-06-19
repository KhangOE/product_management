namespace Product_management.ModelView
{
    public class OrderItemViewModel
    {
        public int ProductId  { get; set; }
        public int Quantity { get; set; }
        public int UnitPrice { get; set; }  
        public int TotalPrice { get; set; } 
    }
}
