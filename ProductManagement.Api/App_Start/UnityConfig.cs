using ProductManagement.Data;
using ProductManagement.Domain;
using ProductManagement.Domain.Service;
using ProductManagement.EntityFramework;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace ProductManagement
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            //Repos
            container.RegisterType<IProductRepository, ProductRepository>();

            // Services
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}