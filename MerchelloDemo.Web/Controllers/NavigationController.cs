namespace MerchelloDemo.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Zone.UmbracoMapper;

    public class NavigationController : BaseSurfaceController
    {
        #region Constructor

        public NavigationController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action methods

        [ChildActionOnly]
        public PartialViewResult Menu(int levels)
        {          
            var vm = new MainMenuViewModel();
            vm.MenuItems = GetMenuItems(GetRootNode(), 0, levels - 1);
            return PartialView("_MainNavigation", vm);
        }        

        [ChildActionOnly]
        public PartialViewResult Breadcrumb()
        {
            var vm = new BreadCrumbViewModel
            {
                CurrentPage = MapItem(CurrentPage),
                MenuItems = CurrentPage
                    .Ancestors()
                    .Where(x => x.Level > 1)
                    .OrderBy(x => x.Level)
                    .Select(x => MapItem(x)),
            };

            return PartialView("_Breadcrumb", vm);
        }

        #endregion

        #region Helpers

        private IEnumerable<MenuItemViewModel> GetMenuItems(IPublishedContent parent, int currentLevel, int maxLevel)
        {
            var menu = parent.Children
                .Where(x => !x.GetPropertyValue<bool>("hideInNavigation"))
                .Select(x =>
                {
                    var item = MapItem(x);

                    if (currentLevel < maxLevel)
                    {
                        item.MenuItems = GetMenuItems(x, currentLevel + 1, maxLevel);
                    }

                    return item;
                });
            return menu;
        }

        public MenuItemViewModel MapItem(IPublishedContent item)
        {
            MenuItemViewModel model = null;

            if (item != null)
            {
                model = new MenuItemViewModel();
                UmbracoMapper.Map(item, model);
                model.IsCurrentPage = CurrentPage.Id.Equals(item.Id);
                model.IsCurrentPageOrAncestor = CurrentPage.Path.Contains(item.Id.ToString());
            }

            return model;
        }

        public MenuItemViewModel MapMenuItems(int levels)
        {
            var root = GetRootNode();
            var menuRoot = MapItem(root);
            menuRoot.MenuItems = GetMenuItems(root, 0, levels - 1).ToList();

            return menuRoot;
        }

        #endregion
    }
}
