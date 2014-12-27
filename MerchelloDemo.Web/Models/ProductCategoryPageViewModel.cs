namespace MerchelloDemo.Web.Models
{
    using System.Collections.Generic;
    using System.Web;

    public class ProductCategoryPageViewModel : BasePageViewModel
    {
        public ProductCategoryPageViewModel()
        {
            Products = new List<ProductPageTeaser>();
        }

        public IHtmlString BodyText { get; set; }

        public IList<ProductPageTeaser> Products { get; set; }
    }
}
