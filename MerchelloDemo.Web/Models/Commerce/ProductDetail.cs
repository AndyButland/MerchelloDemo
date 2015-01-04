namespace MerchelloDemo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ProductDetail
    {
        public Guid Key { get; set; }

        public decimal Price { get; set; }

        public bool HasVariantsWithPriceRange { get; set; }

        public string DisplayPrice
        {
            get
            {
                return (HasVariantsWithPriceRange ? "From " : string.Empty) + Price.ToString("C");
            }
        }        

        public IEnumerable<Option> Options { get; set; }

        public class Option
        {
            public Guid Key { get; set; }

            public string Name { get; set; }

            public SelectList Choices { get; set; }
        }
    }
}
