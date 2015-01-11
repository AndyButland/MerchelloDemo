namespace MerchelloDemo.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Merchello.Core;
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

        public JsonResult GetPriceForProductVariant(Guid productKey, Guid[] optionKeys)
        {
            var merchelloContext = MerchelloContext.Current;
            var product = merchelloContext.Services.ProductService.GetByKey(productKey);
            var variant = merchelloContext.Services.ProductVariantService.GetProductVariantWithAttributes(product, optionKeys);

            var result = new
            {
                price = variant.Price,
                onSale = variant.OnSale,
                salePrice = variant.SalePrice,
            };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion 
    }
}