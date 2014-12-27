namespace MerchelloDemo.Web.Infrastructure.Mapping
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Merchello.Core;
    using Merchello.Web;
    using MerchelloDemo.Web.Helpers;
    using MerchelloDemo.Web.Models;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Zone.UmbracoMapper;

    public class CommerceMapper
    {
        public static object GetProductDetail(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            ProductDetail productDetail = null;
            var productService = MerchelloContext.Current.Services.ProductService;
            var productKey = contentToMapFrom.GetPropertyValue<string>("product");
            if (!string.IsNullOrEmpty(productKey))
            {
                var productKeyAsGuid = Guid.Parse(productKey);
                var product = productService.GetByKey(productKeyAsGuid);
                productDetail = new ProductDetail
                {
                    Key = product.Key,
                    Price = product.Price,
                };

                if (product.ProductOptions.IsAndAny())
                {
                    productDetail.Options = product.ProductOptions
                        .Select(x => new ProductDetail.Option
                        {
                            Key = x.Key,
                            Name = x.Name,
                            Choices = new SelectList(x.Choices
                                .OrderBy(y => y.SortOrder)
                                .ToList(), "Key", "Name"),
                        })
                        .ToList();
                }
            }

            return productDetail;
        }

        public static object GetBasketDetail(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var merchelloContext = MerchelloContext.Current;
            var customerContext = new CustomerContext(UmbracoContext.Current);
            var currentCustomer = customerContext.CurrentCustomer;
            var basket = currentCustomer.Basket();

            return new BasketDetail
            {
                TotalPrice = basket.TotalBasketPrice,
                LineItems = basket.Items
                    .Select(x => new BasketDetail.LineItem
                        {
                            Key = x.Key,
                            Name = x.Name,
                            Quantity = x.Quantity,
                            Price = x.Price,
                        })
                    .OrderBy(x => x.Name)
                    .ToList(),
            };
        }
    }
}
