namespace MerchelloDemo.Web.Models
{
    using System;
    using System.IO;
    using MerchelloDemo.Web.Models.Interfaces;
    using Zone.UmbracoMapper;

    public class BasePageViewModel : BaseNodeViewModel, IMainTitle, IMetaData
    {
        #region Fields

        private string _metaTitle;

        #endregion

        #region Properties

        public string MainHeading { get; set; }

        public virtual string DisplayTitle
        {
            get { return string.IsNullOrEmpty(MainHeading) ? Name : MainHeading; }
        }
        
        #endregion

        #region Metadata

        public string MetaTitle
        {
            get
            {
                return string.IsNullOrEmpty(_metaTitle) ?
                    (string.IsNullOrEmpty(DisplayTitle) ? Name : DisplayTitle)
                    : _metaTitle;
            }

            set
            {
                _metaTitle = value;
            }
        }

        public string MetaDescription { get; set; }

        public string MetaKeywords { get; set; }

        public string CanonicalUrl { get; set; }

        public string AbsoluteUrl { get; set; }

        #endregion
    }
}
