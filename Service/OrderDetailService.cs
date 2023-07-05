using Product_management.Interface.Service;
using Product_management.Models;
using Product_management.unitOfWork;

namespace Product_management.Service
{
    public class OrderDetailService : IOrderDetailService
    {
        private IUnitOfWork _unitOfWork;
        public OrderDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }  
        
        public async Task Create(OrderDetail orderDetail)
        {
            _unitOfWork.OrderDetailRepository.CreateOrderDetail(orderDetail);
        }
    }
}
