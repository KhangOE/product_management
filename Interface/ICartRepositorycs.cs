using Product_management.Models;

namespace Product_management.Interface
{
    public interface ICartRepositorycs
    {
        Task<ICollection<Cart>> GetAll();
        Task<Cart> GetCartById(int id);
        Task DeleteCart(Cart cart);
        Task CreateCart(Cart cart);
        Task UpdateCart(Cart cart);
    }
}
