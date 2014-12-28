namespace MerchelloDemo.Web.UI
{
    using System;
    using MerchelloDemo.Web.Infrastructure.Mapping;
    using Umbraco.Web;

    public class Global : UmbracoApplication
    {
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            AutoMapperMapping.CreateMaps();
        }
    }
}