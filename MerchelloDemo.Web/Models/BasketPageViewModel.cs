namespace MerchelloDemo.Web.Models
{
    using MerchelloDemo.Web.Models.Interfaces;

    public class BasketPageViewModel : BasePageViewModel, IBasket
    {
        public BasketDetail BasketDetail { get; set; }

        public bool AllowBasketEdit
        {
            get
            {
                return true;
            }
        }

        public bool ShowOrderTotal
        {
            get
            {
                return false;
            }
        }

        public string CheckoutPageUrl { get; set; }
    }
}
