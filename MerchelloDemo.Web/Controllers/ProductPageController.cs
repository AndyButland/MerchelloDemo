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

        /// <summary>
        /// Renders the product page
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductPage()
        {
            var vm = GetPageModel<ProductPageViewModel>();
            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Returns JSON for an AJAX request for the pricing information of a product variant
        /// </summary>
        /// <param name="productKey">Product key</param>
        /// <param name="optionKeys">Keys for product options</param>
        /// <returns></returns>
        public JsonResult GetPriceForProductVariant(Guid productKey, Guid[] optionKeys)
        {
            var merchelloContext = MerchelloContext.Current;
            var product = merchelloContext.Services.ProductService.GetByKey(productKey);
            var variant = merchelloContext.Services.ProductVariantService.GetProductVariantWithAttributes(product, optionKeys);

            return Json(new
            {
                price = variant.Price,
                onSale = variant.OnSale,
                salePrice = variant.SalePrice,
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion 
    }
}