using Product_management.Models;

namespace Product_management.Interface
{
    public interface ICartRepositorycs
    {
        ICollection<Cart> GetAll();

        Cart GetCartById(int id);

        bool DeleteCart(Cart cart);
        bool CreateCart(Cart cart);

        bool UpdateCart(Cart cart);

        bool Save();
    }
}
