namespace MerchelloDemo.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using MerchelloDemo.Web.Models;
    using Merchello.Core;
    using Zone.UmbracoMapper;

    public class PaymentPageController : BaseSurfaceController<PaymentPageViewModel>
    {
        #region Constructor

        public PaymentPageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action Methods

        public ActionResult PaymentPage()
        {
            var basket = GetBasket();
            if (basket.IsEmpty)
            {
                return RedirectToBasketPage();
            }

            var vm = GetPageModel<PaymentPageViewModel>();
            
            var paymentMethods = MerchelloContext.Current.Gateways.Payment.GetPaymentGatewayMethods()
                .Select(x => new SelectListItem()
                {
                    Value = x.PaymentMethod.Key.ToString(),
                    Text = x.PaymentMethod.Name
                });
            vm.PaymentMethods = new SelectList(paymentMethods, "Value", "Text");

            return CurrentTemplate(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectPayment(PaymentPageViewModel vm)
        {
            if (ModelState.IsValid)
            {                

                return null;
            }

            return CurrentUmbracoPage();
        }

        #endregion 
    }
}