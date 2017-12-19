[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DotNetWebApi.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DotNetWebApi.NinjectWebCommon), "Stop")]

namespace DotNetWebApi
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Extensions.Conventions;
    using Ninject.Web.WebApi;
    using System.Web.Http;
    using Data.Customers.LLC;
    using Business;
    using Data;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel(new AutomapperModule());
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                // Register the global ninject dependency resolver for controller injection
                // This will resolve the Parameterless Constructor error
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            var thisType = typeof(NinjectWebCommon);

            var webAssembly = typeof(NinjectWebCommon).Assembly;
            var businessAssembly = typeof(IBusinessMarker).Assembly;
            var dataAssembly = typeof(IDataMarker).Assembly;

            var requestScoped = new List<Assembly> { webAssembly, businessAssembly };
            requestScoped.ForEach(a => kernel.Bind(x => x
                .From(a)
                .SelectAllClasses()
                .BindAllInterfaces()
                .Configure(b => b.InRequestScope()))
            );
           

            kernel.Bind(x => x
                .From(dataAssembly)
                .SelectAllClasses()
                .InheritedFrom<ICachingRepository>()
                .BindAllInterfaces()
                .Configure(b => b.InSingletonScope()));


            kernel.Bind(x => x
                .From(dataAssembly)
                .SelectAllClasses()
                .InheritedFrom<IRepository>()
                .BindAllInterfaces()
                .Configure(b => b.InRequestScope()));
               
        }        
    }
}
