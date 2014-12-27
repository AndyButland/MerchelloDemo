namespace MerchelloDemo.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MerchelloDemo.Web.Models;
    using Zone.UmbracoMapper;

    public class ProductCategoryPageController : BaseSurfaceController<ProductCategoryPageViewModel>
    {
        #region Constructor

        public ProductCategoryPageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action Methods

        public ActionResult ProductCategoryPage()
        {
            var vm = GetPageModel<ProductCategoryPageViewModel>();
            UmbracoMapper.MapCollection(CurrentPage.Children, vm.Products);
            return CurrentTemplate(vm);
        }

        #endregion 
    }
}