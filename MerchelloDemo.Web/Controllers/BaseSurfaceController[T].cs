namespace MerchelloDemo.Web.Controllers
{
    using MerchelloDemo.Web.Models;
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
