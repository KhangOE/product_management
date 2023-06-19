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

        public ICollection<Cart> GetAll()
        {
            return  _dataContext.Carts.ToList();
        }

        public void DeleteCart(Cart cart)
        {
            _dataContext.Remove(cart);
          

        }
        public void CreateCart(Cart cart)
        {
           _dataContext.Carts.Add(cart);
          
        }
        public Cart  GetCartById(int id)
        {
            return _dataContext.Carts.FirstOrDefault(x => x.Id == id);
        }
        public void UpdateCart(Cart cart)
        {
            _dataContext.Carts.Update(cart);
        }
    
    }
}
