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
            var basket = GetBasket();
            if (basket.IsEmpty)
            {
                return RedirectToBasketPage();
            }

            if (ModelState.IsValid)
            {
                // Associate the payment method with the order
                var paymentMethod = MerchelloContext.Current.Gateways.Payment.GetPaymentGatewayMethodByKey(vm.SelectedPaymentMethod).PaymentMethod;
                var preparation = basket.SalePreparation();
                preparation.SavePaymentMethod(paymentMethod);
                
                // Authorise the payment
                // TODO: only want to do this for cash payment method, card payments will need other steps.
                var attempt = preparation.AuthorizePayment(paymentMethod.Key);

                // Redirect to receipt page having saved invoice key in session
                if (attempt.Payment.Success)
                {
                    Session["InvoiceKey"] = attempt.Invoice.Key.ToString();
                    return RedirectToUmbracoPage(GetReceiptPageNode().Id);
                }
                else
                {
                    throw new ApplicationException("Payment authorisation failed.");
                }
            }

            return CurrentUmbracoPage();
        }

        #endregion 
    }
}