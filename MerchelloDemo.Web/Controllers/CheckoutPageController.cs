namespace MerchelloDemo.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
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

        #endregion 
    }
}