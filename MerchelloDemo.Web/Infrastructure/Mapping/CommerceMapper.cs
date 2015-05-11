namespace MerchelloDemo.Web.Infrastructure.Mapping
{
    using System;
    using System.Web;
    using Merchello.Core;
    using Merchello.Core.Models;
    using Merchello.Web;
    using Merchello.Web.Models.ContentEditing;
    using MerchelloDemo.Web.Models;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Zone.UmbracoMapper;

    public class CommerceMapper
    {
        /// <summary>
        /// Umbraco mapper custom mapping for mapping product information to a view model
        /// </summary>
        /// <returns></returns>
        public static object GetProductDetail(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var productService = MerchelloContext.Current.Services.ProductService;
            var product = contentToMapFrom.GetPropertyValue<ProductDisplay>("product");
            if (product != null)
            {
                var productDetail = new ProductDetail();
                AutoMapper.Mapper.Map(product, productDetail);
                return productDetail;
            }

            return null;
        }

        /// <summary>
        /// Umbraco mapper custom mapping for mapping basket information to a view model
        /// </summary>
        /// <returns></returns>
        public static object GetBasketDetail(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var umbracoContext = UmbracoContext.Current;
            var customerContext = new CustomerContext(umbracoContext);
            var currentCustomer = customerContext.CurrentCustomer;
            var basket = currentCustomer.Basket();            

            var basketDetail = new BasketDetail();
            AutoMapper.Mapper.Map(basket, basketDetail);

            var preparation = basket.SalePreparation();
            if (preparation.IsReadyToInvoice())
            {
                var invoice = preparation.PrepareInvoice();
                basketDetail.DeliveryPrice = invoice.TotalShipping();
            }

            return basketDetail;           
        }

        /// <summary>
        /// Umbraco mapper custom mapping for mapping invoice information to a view model
        /// </summary>
        /// <returns></returns>
        public static object GetInvoiceDetail(IUmbracoMapper mapper, IPublishedContent contentToMapFrom, string propertyName, bool recursive)
        {
            var invoiceKey = HttpContext.Current.Session["InvoiceKey"] as string;
            if (!string.IsNullOrEmpty(invoiceKey))
            {
                var invoiceKeyAsGuid = Guid.Parse(invoiceKey);
                var invoice = MerchelloContext.Current.Services.InvoiceService.GetByKey(invoiceKeyAsGuid);

                var invoiceDetail = new InvoiceDetail();
                AutoMapper.Mapper.Map(invoice, invoiceDetail);
                return invoiceDetail;     
            }

            return null;
        }
    }
}
