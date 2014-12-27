namespace MerchelloDemo.Web.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using MerchelloDemo.Web.Infrastructure.Mapping;
    using MerchelloDemo.Web.Models;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;
    using Zone.UmbracoMapper;

    public abstract class BaseSurfaceController : SurfaceController, IRenderMvcController
    {
        #region Constructor

        protected BaseSurfaceController(IUmbracoMapper mapper)
        {
            UmbracoMapper = mapper;
            AddCustomMappings();
        }

        #endregion

        #region Properties

        protected IUmbracoMapper UmbracoMapper { get; private set; }

        #endregion

        #region Render MVC

        /// <summary>
        /// Locates the template for the given route
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="model">Instance of model</param>
        /// <param name="viewName">View name</param>
        /// <returns>Template for given route</returns>
        protected ActionResult CurrentTemplate<T>(T model, string viewName = "")
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = ControllerContext.RouteData.Values["action"].ToString();
            }

            return View(viewName, model);
        }

        /// <summary>
        /// Applies the default index action
        /// </summary>
        /// <param name="model">Instance of model</param>
        /// <returns>Default ActionResult</returns>
        public virtual ActionResult Index(RenderModel model)
        {
            return CurrentTemplate(model);
        }

        #endregion

        #region Mapping Helpers

        /// <summary>
        /// Populates a generic model
        /// </summary>
        /// <typeparam name="T">The View-Model</typeparam>
        /// <returns>Populated view model</returns>
        protected virtual T GetModel<T>()
              where T : class, new()
        {
            var model = new T();
            UmbracoMapper.Map(CurrentPage, model);
            return model;
        }

        /// <summary>
        /// Populates a generic page view model
        /// </summary>
        /// <typeparam name="T">The View-Model</typeparam>
        /// <returns>Populated page view model</returns>
        protected virtual T GetPageModel<T>()
             where T : BasePageViewModel, new()
        {
            var model = new T();

            // Set canonical url if requested url differs from current page url
            model.CanonicalUrl = Request.Url == null || Request.Url.AbsolutePath == CurrentPage.Url ? null : CurrentPage.UrlAbsolute();
            model.AbsoluteUrl = CurrentPage.UrlAbsolute();

            // Map page properties
            UmbracoMapper.Map(CurrentPage, model);

            return model;
        }

        /// <summary>
        /// Sets up the custom mappings for the Umbraco Mapper used in the project
        /// </summary>
        private void AddCustomMappings()
        {
            UmbracoMapper
                .AddCustomMapping(typeof(ProductDetail).FullName, CommerceMapper.GetProductDetail)
                .AddCustomMapping(typeof(BasketDetail).FullName, CommerceMapper.GetBasketDetail);
        }

        #endregion

        #region Querying helpers

        /// <summary>
        /// Gets the Umbraco root node (home page)
        /// </summary>
        /// <returns>Instance of IPublishedContent</returns>
        protected IPublishedContent GetRootNode()
        {
            return Umbraco.TypedContentAtRoot().FirstOrDefault();
        }

        /// <summary>
        /// Gets the basket page
        /// </summary>
        /// <returns>Instance of IPublishedContent</returns>
        protected IPublishedContent GetBasketPageNode()
        {
            return GetRootNode()
                .Descendants("BasketPage")
                .FirstOrDefault();
        }

        #endregion
    }
}
