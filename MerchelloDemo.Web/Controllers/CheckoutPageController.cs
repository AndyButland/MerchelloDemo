namespace MerchelloDemo.Web.Controllers
{
    using System.Web.Mvc;
    using Merchello.Core.Models;
    using Merchello.Web;
    using MerchelloDemo.Web.Models;
    using Zone.UmbracoMapper;

    public class CheckoutPageController : BaseSurfaceController<CheckoutPageViewModel>
    {
        #region Constructor

        public CheckoutPageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Renders the checkout page
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckoutPage()
        {
            var basket = GetBasket();
            if (basket.IsEmpty)
            {
                return RedirectToBasketPage();
            }

            var vm = GetPageModel<CheckoutPageViewModel>();
            return CurrentTemplate(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CollectAddress(CheckoutPageViewModel vm)
        {
            var basket = GetBasket();
            if (basket.IsEmpty)
            {
                return RedirectToBasketPage();
            }

            if (ModelState.IsValid)
            {
                // Save billing and shipping addresses
                var address = new Address
                {
                    Address1 = vm.Address1,
                    Address2 = vm.Address2,
                    CountryCode = "GB",
                    Email = vm.Email,
                    Locality = vm.City,
                    Name = vm.CustomerName,
                    Phone = vm.Telephone,
                    PostalCode = vm.Postcode,
                    Region = vm.County,
                };

                var preparation = basket.SalePreparation();
                preparation.SaveBillToAddress(address);
                preparation.SaveShipToAddress(address);

                return RedirectToUmbracoPage(GetDeliveryPageNode().Id);
            }

            return CurrentUmbracoPage();
        }

        #endregion 
    }
}