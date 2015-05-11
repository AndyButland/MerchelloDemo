namespace MerchelloDemo.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Merchello.Core;
    using Merchello.Core.Gateways.Payment;
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

        /// <summary>
        /// Renders the payment page
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Handles the select payment form post
        /// </summary>
        /// <param name="vm">Payment form model</param>
        /// <returns></returns>
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

                // Authorise the payment - if by card, need to collect the card details
                ProcessorArgumentCollection paymentArgs = null;
                if (paymentMethod.Name == AppConstants.CreditCardPaymentMethodName)
                {
                    paymentArgs = new ProcessorArgumentCollection
                    {
                        { "cardholderName", preparation.GetBillToAddress().Name },
                        { "cardNumber", vm.CardDetail.Number },
                        { "expireMonth", vm.CardDetail.ExpiryMonth.ToString() },
                        { "expireYear", vm.CardDetail.ExpiryYear.ToString() },
                        { "cardCode", vm.CardDetail.CVV }
                    };
                }

                var attempt = preparation.AuthorizePayment(paymentMethod.Key, paymentArgs);

                // Redirect to receipt page having saved invoice key in session
                if (attempt.Payment.Success)
                {
                    Session["InvoiceKey"] = attempt.Invoice.Key.ToString();
                    return RedirectToUmbracoPage(GetReceiptPageNode().Id);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Card authorisation failed: " + ParseError(attempt.Payment.Exception.Message));
                }
            }

            return CurrentUmbracoPage();
        }

        private static string ParseError(string exceptionMessage)
        {
            return exceptionMessage.Split('|')[3];
        }

        #endregion
    }
}