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

        /// <summary>
        /// Renders the website navigation
        /// </summary>
        /// <param name="levels">Number of levels</param>
        /// <returns></returns>
        [ChildActionOnly]
        public PartialViewResult Menu(int levels)
        {          
            var vm = new MainMenuViewModel();
            vm.MenuItems = GetMenuItems(GetRootNode(), 0, levels - 1);
            return PartialView("_MainNavigation", vm);
        }        

        /// <summary>
        /// Renders the breadcrumb trail
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Helper to get the menu items for the navigation
        /// </summary>
        /// <param name="parent">Root node</param>
        /// <param name="currentLevel">Current level</param>
        /// <param name="maxLevel">Maximum level to retrieve</param>
        /// <returns></returns>
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

        /// <summary>
        /// Maps a menu item from a content node
        /// </summary>
        /// <param name="item">Content node to map from</param>
        /// <returns></returns>
        private MenuItemViewModel MapItem(IPublishedContent item)
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

        #endregion
    }
}
