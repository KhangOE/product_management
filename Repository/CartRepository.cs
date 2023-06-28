using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;

namespace Product_management.Repository
{
    public class CartRepository : ICartRepositorycs
    {
        private DataContext _dataContext;

        public CartRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }   

        public async Task<ICollection<Cart>> GetAll()
        {
            return await _dataContext.Carts.ToListAsync();
        }

        public async Task DeleteCart(Cart cart)
        {
            _dataContext.Remove(cart);
        }
        public async Task CreateCart(Cart cart)
        {
        
           _dataContext.Carts.Add(cart);
        }

        public async Task<Cart>  GetCartById(int id)
        {
            return await _dataContext.Carts.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task UpdateCart(Cart cart)
        {
            _dataContext.Carts.Update(cart);
        }
    
    }
}
