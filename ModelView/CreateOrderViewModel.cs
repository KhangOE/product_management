using Microsoft.AspNetCore.SignalR;

namespace Product_management.ModelView
{
    public class CreateOrderViewModel
    {
        public int UserId { get; set; }
        public List<OrderItemViewModel> OrderItem { get; set; }
        public int TotalAmount { get; set; }
    }
}
