using Product_management.Models;

namespace Product_management.ModelView
{
    public class ProductViewModel
    {
        public List<ProductItemViewModel> products { get; set; }

        public Product HighBoughProduct { get; set; }  
    }
}
