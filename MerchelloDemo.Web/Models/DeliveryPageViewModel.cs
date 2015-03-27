namespace MerchelloDemo.Web.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using MerchelloDemo.Web.Models.Interfaces;

    public class DeliveryPageViewModel : BasePageViewModel, IBasket
    {
        [Required(ErrorMessage = "Please select a delivery option")]
        [Display(Name = "Delivery option")]
        public Guid SelectedDeliveryOption { get; set; }

        public SelectList DeliveryOptions { get; set; }
        
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
                return false;
            }
        }        
    }
}
