using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;

namespace Product_management.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _context;


        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Product> GetAll()
        {
            return _context.Products
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Order)
                .ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id== id);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove( product );
            
        }
        public void CreateProduct(Product product)
        {

           _context.Products.Add(product);
           
        }
        public void UpdateProduct(Product product) { 
            _context.Update(product);
            
        }
    }
}
