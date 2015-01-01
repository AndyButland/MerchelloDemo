namespace MerchelloDemo.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class PaymentPageViewModel : BasePageViewModel
    {
        [Required(ErrorMessage = "Please select a payment method")]
        [Display(Name = "Payment method")]
        public Guid SelectedPaymentMethod { get; set; }

        public SelectList PaymentMethods { get; set; }
    }
}
