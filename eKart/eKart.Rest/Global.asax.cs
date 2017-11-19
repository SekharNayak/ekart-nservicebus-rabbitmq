using Autofac;
using Autofac.Integration.WebApi;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eKart.Rest
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureEndpoint();
        }

        private void ConfigureEndpoint()
        {
            var endPointConfiguration = new EndpointConfiguration("eKart.Rest");
            endPointConfiguration.UsePersistence<InMemoryPersistence>();
            endPointConfiguration.UseTransport<RabbitMQTransport>().ConnectionStringName("NServiceBus/Transport");
            endPointConfiguration.EnableInstallers();
            endPointConfiguration.PurgeOnStartup(true);
            endPointConfiguration.SendFailedMessagesTo("error");

            // use conventions instead of IEvent , ICommand and IMessage marker interfaces 
            // as it is more elegant .

            var conventions = endPointConfiguration.Conventions();
            conventions.DefiningCommandsAs(t => t.Namespace.StartsWith("eKart") && t.Name.EndsWith("Command"));
            conventions.DefiningEventsAs(t => t.Namespace.StartsWith("eKart") && t.Name.EndsWith("Event"));


            var endPoint = Endpoint.Start(endPointConfiguration).GetAwaiter().GetResult();
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterInstance(endPoint);
            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver =
                new AutofacWebApiDependencyResolver(container);
            // mvcContainerBuilder.RegisterWebApiController();


        }
    }
}
