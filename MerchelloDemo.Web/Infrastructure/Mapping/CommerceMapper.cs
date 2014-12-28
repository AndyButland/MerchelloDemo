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
            var productService = MerchelloContext.Current.Services.ProductService;
            var productKey = contentToMapFrom.GetPropertyValue<string>("product");
            if (!string.IsNullOrEmpty(productKey))
            {
                var productKeyAsGuid = Guid.Parse(productKey);
                var product = productService.GetByKey(productKeyAsGuid);
                var productDetail = new ProductDetail();
                AutoMapper.Mapper.Map(product, productDetail);
                return productDetail;
            }

            return null;
        }

        public static object GetBasketDetail(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var umbracoContext = UmbracoContext.Current;
            var merchelloContext = MerchelloContext.Current;
            var customerContext = new CustomerContext(umbracoContext);
            var currentCustomer = customerContext.CurrentCustomer;
            var basket = currentCustomer.Basket();            

            var basketDetail = new BasketDetail();
            AutoMapper.Mapper.Map(basket, basketDetail);
            return basketDetail;           
        }
    }
}
