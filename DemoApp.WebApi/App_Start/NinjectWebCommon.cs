[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DemoApp.WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DemoApp.WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace DemoApp.WebApi.App_Start
{
    using System;
    using System.Web;
    using AutoMapper;
    using DemoApp.Business;
    using DemoApp.Data.Mapping;
    using DemoApp.IBusiness;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                //System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

                RegisterServices(kernel);
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
            //Register interfaces and repositories
            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>().InSingletonScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();

            //container.RegisterWebApiRequest<INorthwindUnitOfWork, NorthwindUnitOfWork>();
            //container.RegisterWebApiRequest<ICustomerRepository, CustomerRepository>();

            kernel.Bind<IMapper>()
            .ToMethod(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {   //Mapping employee profile
                    cfg.AddProfile<EmployeeMappingProfile>();
                    // tell automapper to use ninject when creating value converters and resolvers
                    cfg.ConstructServicesUsing(t => kernel.Get(t));
                });
            return config.CreateMapper();
                }).InSingletonScope();

        }

    }

}
