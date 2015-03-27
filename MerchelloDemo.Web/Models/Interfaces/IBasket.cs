namespace MerchelloDemo.Web.Models.Interfaces
{
    public interface IBasket
    {
        BasketDetail BasketDetail { get; }

        bool AllowBasketEdit { get; }

        bool ShowOrderTotal { get; }
    }
}
