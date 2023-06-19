using Product_management.Models;

namespace Product_management.Interface
{
    public interface ICartRepositorycs
    {
        ICollection<Cart> GetAll();
        Cart GetCartById(int id);
        void DeleteCart(Cart cart);
        void CreateCart(Cart cart);
        void UpdateCart(Cart cart);
    }
}
