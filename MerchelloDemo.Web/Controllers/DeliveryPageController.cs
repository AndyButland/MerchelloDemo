namespace MerchelloDemo.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Merchello.Core;
    using Merchello.Core.Models;
    using Merchello.Web;
    using MerchelloDemo.Web.Models;
    using Zone.UmbracoMapper;

    public class DeliveryPageController : BaseSurfaceController<DeliveryPageViewModel>
    {
        #region Constructor

        public DeliveryPageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Renders the delivery page
        /// </summary>
        /// <returns></returns>
        public ActionResult DeliveryPage()
        {
            var basket = GetBasket();
            if (basket.IsEmpty)
            {
                return RedirectToBasketPage();
            }

            var vm = GetPageModel<DeliveryPageViewModel>();

            return CurrentTemplate(vm);
        }

        /// <summary>
        /// Handles the select delivery optiom form post
        /// </summary>
        /// <param name="vm">Delivery form model</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectDeliveryOption(DeliveryPageViewModel vm)
        {
            var basket = GetBasket();
            if (basket.IsEmpty)
            {
                return RedirectToBasketPage();
            }

            if (ModelState.IsValid)
            {
                return RedirectToUmbracoPage(GetPaymentPageNode().Id);
            }

            return CurrentUmbracoPage();
        }

        #endregion 
    }
}