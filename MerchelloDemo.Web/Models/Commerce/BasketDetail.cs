namespace MerchelloDemo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MerchelloDemo.Web.Helpers;

    public class BasketDetail
    {
        public decimal TotalProductPrice { get; set; }

        public decimal DeliveryPrice { get; set; }

        public decimal TotalOrderPrice
        {
            get
            {
                return TotalProductPrice + DeliveryPrice;
            }
        }

        public IEnumerable<LineItem> Items { get; set; }

        public bool HasItems
        {
            get
            {
                return Items.IsAndAny();
            }
        }

        public class LineItem
        {
            public Guid Key { get; set; }

            public string Name { get; set; }

            public string ProductPageUrl { get; set; }

            public int Quantity { get; set; } 

            public decimal Price { get; set; }

            public decimal TotalPrice
            {
                get
                {
                    return Quantity * Price;
                }
            }
        }
    }
}
