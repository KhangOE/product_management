using Product_management.Models;

namespace Product_management.Interface
{
    public interface IProductRepository
    {
        ICollection<Product> GetAll();
        Product GetProductById(int id);
        bool DeleteProduct(Product product);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool Save(); 
    }
}
