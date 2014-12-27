namespace MerchelloDemo.Web.Models
{
    using System.Collections.Generic;
    using MerchelloDemo.Web.Models.Interfaces;
    using Zone.UmbracoMapper;

    public class MenuItemViewModel : BaseNodeViewModel, ITitleLink
    {
        public MenuItemViewModel()
        {
        }

        public MenuItemViewModel(BasePageViewModel model)
            : base()
        {
            MainHeading = model.MainHeading;
            Id = model.Id;
            Url = model.Url;
            DocumentTypeAlias = model.DocumentTypeAlias;
            Level = model.Level;
            Name = model.Name;
            SortOrder = model.SortOrder;
        }

        public string NameInNavigation { get; set; }

        public string MainHeading { get; set; }

        public string DisplayTitle
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NameInNavigation)
                        ? NameInNavigation
                        : !string.IsNullOrWhiteSpace(MainHeading)
                            ? MainHeading
                            : Name;
            }
        }

        public bool IsCurrentPage { get; set; }

        public bool IsCurrentPageOrAncestor { get; set; }

        public IEnumerable<MenuItemViewModel> MenuItems { get; set; }

        public bool HasTemplate { get; set; }
    }
}