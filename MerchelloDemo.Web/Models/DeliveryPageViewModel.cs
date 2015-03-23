namespace MerchelloDemo.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class DeliveryPageViewModel : BasePageViewModel
    {
        [Required(ErrorMessage = "Please select a delivery option")]
        [Display(Name = "Delivery option")]
        public Guid SelectedDeliveryOption { get; set; }

        public SelectList DeliveryOptions { get; set; }
    }
}
