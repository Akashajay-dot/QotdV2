using Google;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;
using QOTD.Web.Controllers.Api;
using QOTD.Web.Models;

namespace QOTD.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<AppdbContext>(new HierarchicalLifetimeManager());
            container.RegisterType<AuthController>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}