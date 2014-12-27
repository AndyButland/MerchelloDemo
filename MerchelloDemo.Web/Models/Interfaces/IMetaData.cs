namespace MerchelloDemo.Web.Models.Interfaces
{
    public interface IMetaData
    {
        string MetaTitle { get; }

        string MetaDescription { get; }

        string MetaKeywords { get; }

        string CanonicalUrl { get; }

        string AbsoluteUrl { get; }
    }
}
