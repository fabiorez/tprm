[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TPRM.MVC.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TPRM.MVC.App_Start.NinjectWebCommon), "Stop")]

namespace TPRM.MVC.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using TPRM.Application.Interface;
    using TPRM.Application;
    using TPRM.Domain.Interfaces.Services;
    using TPRM.Domain.Services;
    using TPRM.Domain.Interfaces.Repositories;
    using TPRM.Infra.Data.Repositorios;

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
            kernel.Bind(typeof(IAppServiceBase<>)).To(typeof(AppServiceBase<>));
            kernel.Bind(typeof(IServiceBase<>)).To(typeof(ServiceBase<>));
            kernel.Bind(typeof(IRepositoryBase<>)).To(typeof(RepositoryBase<>));

            kernel.Bind<IServicoRepository>().To<ServicoRepository>();
            kernel.Bind<IServicoAppService>().To<ServicoAppService>();
            kernel.Bind<IServicoService>().To<ServicoService>();

            kernel.Bind<ITransacaoRepository>().To<TransacaoRepository>();
            kernel.Bind<ITransacaoAppService>().To<TransacaoAppService>();
            kernel.Bind<ITransacaoService>().To<TransacaoService>();

            kernel.Bind<IEmpresaRepository>().To<EmpresaRepository>();
            kernel.Bind<IEmpresaAppService>().To<EmpresaAppService>();
            kernel.Bind<IEmpresaService>().To<EmpresaService>();
        }
    }
}
