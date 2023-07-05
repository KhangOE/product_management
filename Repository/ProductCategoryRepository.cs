using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;

namespace Product_management.Repository
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private DataContext _dataContext;
        public ProductCategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task Create(ProductCategory productCategory)
        {
            _dataContext.Add(productCategory);
        }
    }
}
