namespace MerchelloDemo.Web.Models
{
    using System.Collections.Generic;

    public class BreadCrumbViewModel
    {
        public MenuItemViewModel CurrentPage { get; set; }

        public IEnumerable<MenuItemViewModel> MenuItems { get; set; }
    }
}
