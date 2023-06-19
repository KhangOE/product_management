using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Repository;

namespace Product_management.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IProductRepository _productRepository;
        private  IOrderRepository _orderRepository;
        private  IUserRepository _userRepository;
        private  ICartRepositorycs _cartRepository;
        private readonly DataContext _dataContext;

        public UnitOfWork(IProductRepository productRepository, IOrderRepository orderRepository, IUserRepository userRepository, ICartRepositorycs cartRepository,DataContext dataContext)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _dataContext = dataContext;
        }

        public IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = new ProductRepository(_dataContext);
                  
                }
                return _productRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if (this._orderRepository == null)
                {
                    this._orderRepository = new OrderRepository(_dataContext);

                }
                return _orderRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (this._userRepository == null)
                {
                    this._userRepository = new UserRepository(_dataContext);

                }
                return _userRepository;
            }
        }

        public ICartRepositorycs cartRepositorycs
        {
            get
            {
                if (this._cartRepository == null)
                {
                    this._productRepository = new ProductRepository(_dataContext);

                }
                return _cartRepository;
            }
        }

        public void Save()
        {
            _dataContext.SaveChanges();
        }

        
        public async Task<int> CommitChangesAsync() => await _dataContext.SaveChangesAsync();

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dataContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
