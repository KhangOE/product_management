using Product_management.Data;
using Product_management.Interface.Service;
using Product_management.Models;
using Product_management.unitOfWork;

namespace Product_management.Service
{
    public class ProductService : IProductService
    {
        private IUnitOfWork _unitOfWork;
        private DataContext _dataContext;
       

        public ProductService(IUnitOfWork unitOfWork,DataContext dataContext)
        {
            _unitOfWork = unitOfWork;
            _dataContext = dataContext;
        }

        public async Task<ICollection<Product>> GetProducts(){

            return await _unitOfWork.ProductRepository.GetAll();
         }

        public async Task Create(Product product,List<int> CategoryIds)
        {
            HashSet<int> categories = _dataContext.Categories.Select(x => x.id).ToHashSet();
            using var transactions =  _unitOfWork.dbTransaction();
            try
            {
                _unitOfWork.ProductRepository.CreateProduct(product);
                await _unitOfWork.SaveChangesAsync();
                
                var lockObject= new object();
                //chuyen danh sach category id thanh hashset không còn trùng nhau 
                HashSet<int> _categoryIds = new HashSet<int>(CategoryIds);

                // category dau vao khoong thuoc category
                if (!_categoryIds.IsSubsetOf(categories))
                {
                    throw new Exception();
                }

                Parallel.ForEach(_categoryIds, (id) =>
                {
                    var productCategory = new ProductCategory() { 
                        CategoryId = id,
                        Product = product,
                    };

                    lock(lockObject)
                    {
                        // _unitOfWork.ProductCategoryRepository.Create(productCategory);
                        _dataContext.Add(productCategory);
                    };
                });
                
                await _unitOfWork.SaveChangesAsync();
                transactions.Commit();
            }
            catch (Exception ex)
            {
                transactions.Rollback();
            }
            finally { 
            }  
        }

        public async Task<Product> GetProduct(int id) { 
            return await _unitOfWork.ProductRepository.GetProductById(id);
        }
    }
}
