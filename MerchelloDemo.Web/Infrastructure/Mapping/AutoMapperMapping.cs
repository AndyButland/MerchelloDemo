namespace MerchelloDemo.Web.Infrastructure.Mapping
{
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using Merchello.Core.Models;
    using Merchello.Web.Workflow;
    using MerchelloDemo.Web.Models;
    using Umbraco.Web;

    public static class AutoMapperMapping
    {
        public static void CreateMaps()
        {
            var umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            Mapper.CreateMap<IProduct, ProductDetail>()
                .ForMember(dest => dest.Options,
                           source => source.MapFrom(src => src.ProductOptions));
            Mapper.CreateMap<IProductOption, ProductDetail.Option>();
            Mapper.CreateMap<ProductAttributeCollection, SelectList>()
                .ConstructUsing(x => new SelectList(x
                    .OrderBy(y => y.SortOrder)
                    .ToList(), "Key", "Name"));

            Mapper.CreateMap<IBasket, BasketDetail>()
                .ForMember(dest => dest.TotalPrice,
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
