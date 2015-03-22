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

        /// <summary>
        /// Renders the basket page
        /// </summary>
        /// <returns></returns>
        public ActionResult BasketPage()
        {
            var vm = GetPageModel<BasketPageViewModel>();
            vm.CheckoutPageUrl = GetCheckoutPageNode().Url;

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Adds an item to the basket
        /// </summary>
        /// <param name="productKey">Product key</param>
        /// <param name="contentId">Related Umbraco content item Id</param>
        /// <param name="optionKeys">Keys for product options</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the quantity of a basket line item
        /// </summary>
        /// <param name="itemKey">Basket line item key</param>
        /// <param name="quantity">New quantity</param>
        /// <returns></returns>
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

        /// <summary>
        /// Removes an item from the basket
        /// </summary>
        /// <param name="itemKey">Line item key</param>
        /// <returns></returns>
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