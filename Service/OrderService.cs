
using Product_management.Models;
using Product_management.ModelView;
using Product_management.unitOfWork;
using Product_management.Interface.Service;
using Product_management.Data;
using Microsoft.IdentityModel.Tokens;

namespace Product_management.Service
{
    public class OrderService : IOrderServicecs
    {
        private IUnitOfWork _unitOfWork;
        private DataContext _dataContext;
        public OrderService(IUnitOfWork unitOfWork,DataContext dataContext)
        {
            _unitOfWork = unitOfWork;
            _dataContext = dataContext;
        }

        
        public async Task<ICollection<Order>> GetOrders()
        {
            var orders = await _unitOfWork.OrderRepository.GetAll();
            return orders;
        }
        public async Task Delete(Order order)
        {
            _unitOfWork.OrderRepository.DeleteOrder(order);
        }
        public async Task<Order> GetOrder(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(id);
            return order;
        }

        public async Task<Dictionary<int, string>> GetSplierAvailable()
        {
            var supplier = new Dictionary<int, string>() {
                { 1,"s1"},
                {2,"s2" },
                {3,"s3" }
            };
            return supplier;
        }
        public async Task<HashSet<Address>> AdressAvailble(int UserId, HashSet<OrderItemViewModel> orderItemViewModels) {
            var user = await _unitOfWork.UserRepository.GetUserById(UserId);
            var AddressAvailable = new HashSet<Address>();
          
            //check add dress user vs adress available product
          foreach(var uad in user.Adress)
           {
              var check = true;
              foreach(var odi in orderItemViewModels)
                {
                    var _product = await _unitOfWork.ProductRepository.GetProductById(odi.ProductId);
                    if (!_product.AddressesIdAvailable.Contains(uad.Key))
                    {
                        check = false; break;
                    }
                }
              if (check )
                {
                    AddressAvailable.Add(uad.Value);
                }
           }
            return AddressAvailable;
        }

        async Task<HashSet<int>> categoryDiscountThisMonth()
        {
            return new HashSet<int>(){1,2,3,4};
        }

        public async Task<bool> checkSplier(HashSet<OrderItemViewModel> orderItemViewModels)
        {
            Dictionary<int,string> supplier = await this.GetSplierAvailable();

            foreach(var odi in orderItemViewModels)
            {
                var product = await _unitOfWork.ProductRepository.GetProductById(odi.ProductId);
                if (!supplier.ContainsKey(product.SupplierId)){
                    return false;
                }
            }
            return true;
        }

        public async Task Create(int UserId,int TotalAmount, List<OrderItemViewModel> orderItemViewModels)
        {
            //chuyển thành hashset tránh trùng lập 
            HashSet<OrderItemViewModel> _orderItems = new HashSet<OrderItemViewModel>(orderItemViewModels);

            var addressAvailable = await AdressAvailble(UserId, _orderItems);
            Console.WriteLine("#####################" + addressAvailable.Count);
            foreach(var odi in addressAvailable)
            {
                Console.WriteLine("#####################" + addressAvailable);

                Console.WriteLine(odi.Id.ToString());
            }
            
            if(addressAvailable.IsNullOrEmpty() ) {
                throw new Exception();
             }
            
            if(!await checkSplier(_orderItems))
            {
                throw new Exception();
            }
           
           var order = new Order() { 
            Total = TotalAmount,
            UserId = UserId,
           };
            var product  = await _unitOfWork.ProductRepository.GetAll();
          
            try
            {
                await _unitOfWork.OrderRepository.CreateOrder(order);
                await _unitOfWork.SaveChangesAsync();
                Console.WriteLine("start");
                object lockObject = new object();
                Parallel.ForEach(
                    _orderItems
                    , (item) =>
                    {
                        var od = new OrderDetail()
                        {
                            TotalPrice = item.TotalPrice,
                            UnitPrice = item.UnitPrice,
                            ProductId = item.ProductId,
                            quantity = item.Quantity,
                            Order = order
                        };
                        lock (lockObject)
                        {
                            _unitOfWork.OrderDetailRepository.CreateOrderDetail(od);
                        }
                    }
                );
                
                Console.WriteLine("end");
                //   _dataContext.SaveChanges();
                await _unitOfWork.SaveChangesAsync();
            //    transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
             //   transaction.Rollback();
            }
            finally {
                //_dataContext.Dispose();
                }


        }


    }
}
