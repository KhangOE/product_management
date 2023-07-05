using Microsoft.EntityFrameworkCore;
using Product_management.Data;
using Product_management.Interface;
using Product_management.Models;

namespace Product_management.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<ICollection<Category>> GetCategories()
        {
           return await _dataContext.Categories.ToListAsync();
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _dataContext.Categories.FirstOrDefaultAsync(x => x.id == id);
        }
    }
}
