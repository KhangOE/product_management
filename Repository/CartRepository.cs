using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;

namespace Product_management.Repository
{
    public class CartRepository : ICartRepositorycs
    {
        DataContext _dataContext;

        public CartRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }   

        public ICollection<Cart> GetAll()
        {
            return  _dataContext.Carts.ToList();
        }

        public bool DeleteCart(Cart cart)
        {
            _dataContext.Remove(cart);
            return Save();

        }
        public bool CreateCart(Cart cart)
        {
           _dataContext.Carts.Add(cart);
            return Save();
        }
        public Cart  GetCartById(int id)
        {
            return _dataContext.Carts.FirstOrDefault(x => x.Id == id);
        }
        public bool UpdateCart(Cart cart)
        {
            _dataContext.Carts.Update(cart);
            return Save();
        }
        

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
