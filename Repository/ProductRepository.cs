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

        public bool DeleteProduct(Product product)
        {
            _context.Products.Remove( product );
            return Save();
        }
        public bool CreateProduct(Product product)
        {

           _context.Products.Add(product);
            return Save();
        }
        public bool UpdateProduct(Product product) { 
            _context.Update(product);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        } 
    }
}
