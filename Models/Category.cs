namespace Product_management.Models
{
    public class Category
    {
        public int id { get; set; }
        public int Name { get; set; }
        public ICollection<ProductCategory> ProductCategorys { get; set; }


    }
}
