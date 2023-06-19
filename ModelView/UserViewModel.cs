using Product_management.Models;

namespace Product_management.ModelView
{
    public class UserViewModel
    {
       public  List<UserItemViewModel> userItemViewModels { get; set; }
       public User HighestUser { get; set; }
    }
}
