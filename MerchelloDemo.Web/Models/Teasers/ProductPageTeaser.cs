namespace MerchelloDemo.Web.Models
{
    using Zone.UmbracoMapper;

    public class ProductPageTeaser : BaseNodeViewModel
    {
        public string Summary { get; set; }

        public ProductDetail ProductDetail { get; set; }
    }
}
