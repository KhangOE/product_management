using Product_management.Models;

namespace Product_management.Interface
{
    public interface IProductRepository
    {
        ICollection<Product> GetAll();
        Product GetProductById(int id);
        void DeleteProduct(Product product);
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
       
    }
}
