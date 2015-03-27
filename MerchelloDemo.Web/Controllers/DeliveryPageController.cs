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

            // Package into shipments (we'll only have one)
            var shipment = basket.PackageBasket(basket.SalePreparation().GetBillToAddress()).First();

            var vm = GetPageModel<DeliveryPageViewModel>();

            var deliveryOptions = shipment.ShipmentRateQuotes()
                .OrderBy(x => x.Rate)
                .Select(x => new SelectListItem()
                {
                    Value = x.ShipMethod.Key.ToString(),
                    Text = x.ShipMethod.Name + " $" + x.Rate.ToString("N2"),
                });
            vm.DeliveryOptions = new SelectList(deliveryOptions, "Value", "Text");

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
                // Save selected delivery option
                var shipment = basket.PackageBasket(basket.SalePreparation().GetBillToAddress()).First();
                var deliveryOption = shipment.ShipmentRateQuotes()
                    .Single(x => x.ShipMethod.Key == vm.SelectedDeliveryOption);

                var preparation = basket.SalePreparation();
                preparation.ClearShipmentRateQuotes();
                preparation.SaveShipmentRateQuote(deliveryOption);

                return RedirectToUmbracoPage(GetPaymentPageNode().Id);
            }

            return CurrentUmbracoPage();
        }

        #endregion 
    }
}