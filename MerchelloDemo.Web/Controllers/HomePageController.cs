namespace MerchelloDemo.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using MerchelloDemo.Web.Models;
    using Zone.UmbracoMapper;

    public class HomePageController : BaseSurfaceController<HomePageViewModel>
    {
        #region Constructor

        public HomePageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Renders the home page
        /// </summary>
        /// <returns></returns>
        public ActionResult HomePage()
        {
            var vm = GetPageModel<HomePageViewModel>();
            return CurrentTemplate(vm);
        }

        #endregion 
    }
}