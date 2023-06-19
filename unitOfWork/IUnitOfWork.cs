using Product_management.Data;
using Product_management.Interface;

namespace Product_management.unitOfWork
{
    public interface IUnitOfWork
    {
        //DataContext DataContext { get; set; }
        IProductRepository ProductRepository { get;  }

        IUserRepository UserRepository { get;  }

        IOrderRepository OrderRepository { get; }

        ICartRepositorycs cartRepositorycs { get; }
        void Save();
    }
}
