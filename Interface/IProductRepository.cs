using Product_management.Models;
using Product_management.ModelView;

namespace Product_management.Interface
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAll();
        Product HighestBough();
        Task<Product> GetProductById(int id);
        Task DeleteProduct(Product product);
        Task CreateProduct(Product product);
        Task UpdateProduct(Product product);
       
    }
}
