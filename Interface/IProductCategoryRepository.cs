using Product_management.Models;

namespace Product_management.Interface
{
    public interface IProductCategoryRepository
    {
        Task Create(ProductCategory productCategory);
    }
}
