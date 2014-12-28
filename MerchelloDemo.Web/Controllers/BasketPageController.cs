namespace MerchelloDemo.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Merchello.Core;
    using Merchello.Core.Models;
    using MerchelloDemo.Web.Helpers;
    using MerchelloDemo.Web.Models;
    using Zone.UmbracoMapper;

    public class BasketPageController : BaseSurfaceController<BasketPageViewModel>
    {
        #region Constructor

        public BasketPageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action Methods

        public ActionResult BasketPage()
        {
            var vm = GetPageModel<BasketPageViewModel>();
            vm.CheckoutPageUrl = GetCheckoutPageNode().Url;

            return CurrentTemplate(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddItem(Guid productKey, int contentId, Guid[] optionKeys = null)
        {
            var merchelloContext = MerchelloContext.Current;
            var basket = GetBasket(merchelloContext);

            // Get requested product
            var product = merchelloContext.Services.ProductService.GetByKey(productKey);

            // Save content Id along with line item
            var extendedData = new ExtendedDataCollection();
            extendedData.SetValue("umbracoContentId", contentId.ToString());

            // Add product to basket 
            if (optionKeys.IsAndAny())
            {
                // If options provided, work out the content variant that has been requested
                var variant = merchelloContext.Services.ProductVariantService.GetProductVariantWithAttributes(product, optionKeys);
                basket.AddItem(variant, variant.Name, 1, extendedData);
            }
            else
            {
                basket.AddItem(product, product.Name, 1, extendedData);
            }

            basket.Save();

            return RedirectToBasketPage();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateItemQuantity(Guid itemKey, int quantity)
        {
            var basket = GetBasket();

            // Validate requested item in basket and remove it
            if (basket.Items.FirstOrDefault(x => x.Key == itemKey) != null)
            {
                basket.UpdateQuantity(itemKey, quantity);
                basket.Save();
            }

            return RedirectToBasketPage();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveItem(Guid itemKey)
        {
            var basket = GetBasket();

            // Validate requested item in basket and remove it
            if (basket.Items.FirstOrDefault(x => x.Key == itemKey) != null)
            {
                basket.RemoveItem(itemKey);
                basket.Save();
            }

            return RedirectToBasketPage();
        }

        #endregion 
    }
}