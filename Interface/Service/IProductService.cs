using Product_management.Models;

namespace Product_management.Interface.Service
{
    public interface IProductService
    {
        Task<ICollection<Product>> GetProducts();

        Task<Product> GetProduct(int id);

        Task Create(Product product,List<int> CategoryIds);
    }
}
