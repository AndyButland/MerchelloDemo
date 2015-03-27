namespace MerchelloDemo.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using MerchelloDemo.Web.Models.Interfaces;

    public class PaymentPageViewModel : BasePageViewModel, IBasket
    {
        [Required(ErrorMessage = "Please select a payment method")]
        [Display(Name = "Payment method")]
        public Guid SelectedPaymentMethod { get; set; }

        public SelectList PaymentMethods { get; set; }

        public BasketDetail BasketDetail { get; set; }

        public bool AllowBasketEdit
        {
            get
            {
                return false;
            }
        }

        public bool ShowOrderTotal
        {
            get
            {
                return true;
            }
        }
    }
}
