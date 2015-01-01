namespace MerchelloDemo.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CheckoutPageViewModel : BasePageViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        [Display(Name = "Your name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [Display(Name = "Your email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your telephone")]
        [Display(Name = "Your telephone number")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Please enter the first line of your address")]
        [Display(Name = "Address line 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address line 2")]
        public string Address2 { get; set; }

        [Required(ErrorMessage = "Please enter your city or town")]
        [Display(Name = "Town/city")]
        public string City { get; set; }

        public string County { get; set; }

        [Required(ErrorMessage = "Please enter your postcode")]
        public string Postcode { get; set; }
    }
}
