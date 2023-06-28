using Product_management.Data;
using Product_management.Interface;
using System.Data;

namespace Product_management.unitOfWork
{
    public interface IUnitOfWork
    {
        //DataContext DataContext { get; set; }
        IProductRepository ProductRepository { get;  }

        IUserRepository UserRepository { get;  }
        IDbTransaction dbTransaction();
        IOrderRepository OrderRepository { get; }
        IOrderIDetailRepositorycs OrderDetailRepository { get; }

        Task SaveChangesAsync();

        ICartRepositorycs cartRepositorycs { get; }
        void Save();
    }
}
