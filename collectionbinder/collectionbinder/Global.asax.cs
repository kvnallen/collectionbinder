using System.Collections.Generic;
using collectionbinder.Binders;
using collectionbinder.Models;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace collectionbinder
{

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //CollectionConfig.Add<Phone>();
            //CollectionConfig.Add<Email>();
            ModelBinders.Binders.DefaultBinder = new KevinBinder();
            /*ModelBinders.Binders.Add(typeof(IEnumerable<Phone>), new CollectionBinder<Phone>());
            ModelBinders.Binders.Add(typeof(ICollection<Phone>), new CollectionBinder<Phone>());
            ModelBinders.Binders.Add(typeof(IEnumerable<Email>), new CollectionBinder<Email>());*/
        }
    }
}