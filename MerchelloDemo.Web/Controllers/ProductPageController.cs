namespace MerchelloDemo.Web.Controllers
{
    using System.Web.Mvc;
    using MerchelloDemo.Web.Models;
    using Zone.UmbracoMapper;

    public class ProductPageController : BaseSurfaceController<ProductPageViewModel>
    {
        #region Constructor

        public ProductPageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action Methods

        public ActionResult ProductPage()
        {
            var vm = GetPageModel<ProductPageViewModel>();
            return CurrentTemplate(vm);
        }

        #endregion 
    }
}