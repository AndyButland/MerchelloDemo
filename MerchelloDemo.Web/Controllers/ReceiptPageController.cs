namespace MerchelloDemo.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Merchello.Core;
    using Merchello.Core.Models;
    using Merchello.Web;
    using MerchelloDemo.Web.Models;
    using Zone.UmbracoMapper;

    public class ReceiptPageController : BaseSurfaceController<ReceiptPageViewModel>
    {
        #region Constructor

        public ReceiptPageController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }

        #endregion

        #region Action Methods

        /// <summary>
        /// Renders the receipt page
        /// </summary>
        /// <returns></returns>
        public ActionResult ReceiptPage()
        {
            var vm = GetPageModel<ReceiptPageViewModel>();
            return CurrentTemplate(vm);
        }

        #endregion 
    }
}