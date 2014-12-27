namespace MerchelloDemo.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using MerchelloDemo.Web.Models;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;
    using Zone.UmbracoMapper;

    public abstract class BaseSurfaceController<T> : BaseSurfaceController
            where T : BasePageViewModel, new()
    {
        protected BaseSurfaceController(IUmbracoMapper mapper)
            : base(mapper)
        {
        }
    }
}
