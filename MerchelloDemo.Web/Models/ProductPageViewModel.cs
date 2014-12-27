namespace MerchelloDemo.Web.Models
{
    using System.Web;

    public class ProductPageViewModel : BasePageViewModel
    {
        public IHtmlString BodyText { get; set; }

        public ProductDetail ProductDetail { get; set; }
    }
}
