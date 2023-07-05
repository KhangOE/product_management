using Product_management.Models;

namespace Product_management.Interface
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategories();
        Task<Category> GetCategory(int id);
    }
}
