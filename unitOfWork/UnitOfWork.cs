using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Repository;
using System.Data;

namespace Product_management.unitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private  IProductRepository _productRepository;
        private  IOrderRepository _orderRepository;
        private  IUserRepository _userRepository;
        private  ICartRepositorycs _cartRepository;
        private  IOrderIDetailRepositorycs _orderDetailRepository;
        private readonly DataContext _dataContext;

        public UnitOfWork(IOrderIDetailRepositorycs orderIDetailRepositorycs,IProductRepository productRepository, IOrderRepository orderRepository, IUserRepository userRepository, ICartRepositorycs cartRepository,DataContext dataContext)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _cartRepository = cartRepository;
            _dataContext = dataContext;
            _orderDetailRepository = orderIDetailRepositorycs;
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

        public IDbTransaction dbTransaction()
        {
            var transaction = _dataContext.Database.BeginTransaction();

            return transaction.GetDbTransaction();
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

        public IOrderIDetailRepositorycs OrderDetailRepository
        {
            get
            {
                if (this._orderDetailRepository == null)
                {
                    this._orderDetailRepository = new OrderDetailRepository(_dataContext);

                }
                return _orderDetailRepository;
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

        public async Task SaveChangesAsync()
        {
            await _dataContext.SaveChangesAsync();
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
