namespace MerchelloDemo.Web.Infrastructure.Mapping
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Merchello.Core.Models;
    using Merchello.Web.Models.ContentEditing;
    using Merchello.Web.Workflow;
    using MerchelloDemo.Web.Models;
    using Umbraco.Web;

    public static class AutoMapperMapping
    {
        /// <summary>
        /// Creates AutoMapper mapping definitions for mapping Merchello commerce classes to view model classes
        /// </summary>
        public static void CreateMaps()
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            Mapper.CreateMap<ProductDisplay, ProductDetail>()
                .ForMember(dest => dest.Options,
                           source => source.MapFrom(src => src.ProductOptions))
                .ForMember(dest => dest.HasVariantsWithPriceRange,
                           source => source.MapFrom(src => src.ProductVariants.Min(x => x.Price) < src.ProductVariants.Max(x => x.Price)));
            Mapper.CreateMap<ProductOptionDisplay, ProductDetail.Option>();
            Mapper.CreateMap<IEnumerable<ProductAttributeDisplay>, SelectList>()
                .ConstructUsing(x => new SelectList(x
                    .OrderBy(y => y.SortOrder)
                    .ToList(), "Key", "Name"));

            Mapper.CreateMap<IBasket, BasketDetail>()
                .ForMember(dest => dest.TotalProductPrice,
                           source => source.MapFrom(src => src.TotalBasketPrice));
            Mapper.CreateMap<ILineItem, BasketDetail.LineItem>()
                .ForMember(dest => dest.ProductPageUrl,
                           source => source.MapFrom(src => umbracoHelper.TypedContent(int.Parse(src.ExtendedData["umbracoContentId"])).Url));

            Mapper.CreateMap<IInvoice, InvoiceDetail>()
                .ForMember(dest => dest.InvoiceStatus,
                           source => source.MapFrom(src => src.InvoiceStatus.Name));
        }
    }
}
