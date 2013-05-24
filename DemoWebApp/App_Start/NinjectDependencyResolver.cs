using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace DemoWebApp
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }


        public object GetService(Type serviceType)
        {
            try
            {
                return _kernel.Get(serviceType);
            }
            catch(ActivationException e)
            {
                // pas possible de le résoudre ... on dit que c'est pas grave !
                return null; // that's how DependencyResolver rolls ...
            }

        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
            
        }
    }
}