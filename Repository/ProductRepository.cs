using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;
using Product_management.ModelView;

namespace Product_management.Repository
{
    public class ProductRepository : IProductRepository
    {
        private DataContext _context;


        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Product>> GetAll()
        {
            return await _context.Products
                .Include(x => x.OrderDetails)
                .ThenInclude(x => x.Order)
                .ToListAsync();
        }

       
        public  Product HighestBough()
        {
            var currentTime = DateTime.Now;


            //  var  highestBoughProduct lambda syntax
            var products = _context.Products.ToList();
            var HighBoughProduct = products
              .MaxBy(x => x.OrderDetails.Sum(y => y.quantity));


            // in entity syntax
            var HigBougjProductQuerySyntax = (from product in products
                                     where product.OrderDetails
                                           .Sum(x => x.quantity) 
                                           == products
                                           .Max(x => x.OrderDetails
                                                .Sum(x => x.quantity))
                                     select product).FirstOrDefault();
                                   
            return  HighBoughProduct;
        }


       
        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteProduct(Product product)
        {
            
             _context.Products.Remove(product);
            
        }
        public async Task CreateProduct(Product product)
        {

           _context.Products.Add(product);
           
        }
        public async Task UpdateProduct(Product product) { 
            _context.Update(product);
            
        }
    }
}
