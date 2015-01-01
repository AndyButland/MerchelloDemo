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
            if (ModelState.IsValid)
            {
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

                var basket = GetBasket();
                basket.SalePreparation().SaveBillToAddress(address);
                basket.SalePreparation().SaveShipToAddress(address);

                return RedirectToUmbracoPage(GetPaymentPageNode().Id);
            }

            return CurrentUmbracoPage();
        }

        #endregion 
    }
}