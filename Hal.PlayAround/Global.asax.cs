using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using Hal.PlayAround.Repositories;
using WebApi.Hal;

namespace Hal.PlayAround
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute("Api", "{controller}/{id}", new { id = RouteParameter.Optional });
        }
    }

    public static class MvcConfig
    {
        public static void Register(RouteCollection routes)
        {
            routes.MapRoute("Homepage", "", new {controller = "Home", action = "Index"});
        }
    }

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            MvcConfig.Register(RouteTable.Routes);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PersonRepository>().AsImplementedInterfaces().SingleInstance();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            HalConfiguration.Configure(GlobalConfiguration.Configuration);
        }
    }
}