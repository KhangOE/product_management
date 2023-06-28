using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;

namespace Product_management.Repository
{
    public class OrderDetailRepository : IOrderIDetailRepositorycs
    {

        private readonly DataContext _dataContext;

        public OrderDetailRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }

       
        public async Task<List<OrderDetail>> GetAll()
        {
            return await _dataContext.OrderDetails.ToListAsync();
        }

        public async Task CreateOrderDetail(OrderDetail orderDetail)
        {
          await  _dataContext.OrderDetails.AddAsync(orderDetail);
        }
    }
}
