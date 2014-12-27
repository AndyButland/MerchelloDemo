﻿namespace MerchelloDemo.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class BasketDetail
    {
        public decimal TotalPrice { get; set; } 

        public IEnumerable<LineItem> LineItems { get; set; }

        public class LineItem
        {
            public Guid Key { get; set; }

            public string Name { get; set; }

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