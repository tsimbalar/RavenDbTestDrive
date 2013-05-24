using System.Web.Mvc;
using Abstracts;
using Ninject;
using Raven.Client;
using Raven.Client.Document;
using RavenDbDal;

namespace DemoWebApp
{
    public class IocConfig
    {
        private static StandardKernel _kernel = new StandardKernel();

        public static void Initialize()
        {
            ConfigureDependencies((StandardKernel) _kernel);
            var ninjectResolver = new NinjectDependencyResolver(_kernel);

            DependencyResolver.SetResolver(ninjectResolver);
        }

        private static void ConfigureDependencies(StandardKernel kernel)
        {
            kernel.Bind<IDocumentStore>().ToMethod(context =>
            {
                var documentStore = new DocumentStore { ConnectionStringName = "RavenDbConnection" };
                return documentStore.Initialize();
            }).InSingletonScope(); // only one document store !

            kernel.Bind<IDocumentSession>()
                  .ToMethod(c => 
                      {
                          var store = c.Kernel.Get<IDocumentStore>();
                          return store.OpenSession();
                      }).InThreadScope() // only one per thread (reuse session in the same request !)
                  ;

            kernel.Bind<ICustomerRepository>()
                .To<CustomerRepository>();

        }
    }
}