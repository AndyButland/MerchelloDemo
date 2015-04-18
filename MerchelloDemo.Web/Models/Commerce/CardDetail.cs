namespace MerchelloDemo.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CardDetail
    {
        // TODO: add RequiredIf validation using expressive annotations
        [StringLength(16, ErrorMessage = "Your card number cannnot be longer than 16 digits")]
        public string Number { get; set; }

        [StringLength(4, ErrorMessage = "Your CVV number  cannnot be longer than 4 digits")]
        public string CVV { get; set; }

        [Range(1, 12, ErrorMessage = "The expiry month must be a number between 1 and 12")]
        public byte? ExpiryMonth { get; set; }

        public short? ExpiryYear { get; set; }
    }
}
