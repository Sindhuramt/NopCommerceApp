using Autofac;
using Autofac.Integration.Mvc;
using Libraries.Data;
using Libraries.Repository.Implementation;
using Libraries.Repository.Interfaces;
using System.Web.Mvc;
using System.Web.Routing;

namespace NopCommerceApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            var builder = new ContainerBuilder();
            builder.RegisterType<NopCommerceContext>().AsSelf().InstancePerRequest();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            // Register your services/interfaces here using builder.RegisterType<YourService>().As<IYourService>();
            builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<CartRepository>().As<ICartRepository>();
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }

    }
}
