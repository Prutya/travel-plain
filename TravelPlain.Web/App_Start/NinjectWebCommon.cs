[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(TravelPlain.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(TravelPlain.Web.App_Start.NinjectWebCommon), "Stop")]

namespace TravelPlain.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Business.Infrastructure;
    using AutoMapper;
    using Business.DTO;
    using System.Collections.Generic;
    using Business.Interfaces;
    using Business.Services;
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
            kernel.Load(new ServiceModule("TravelPlainConnection"));

            kernel.Bind<ITourService>().To<TourService>();
            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<IOrderService>().To<OrderService>();
            kernel.Bind<IBusinessValuesService>().To<BusinessValuesService>();

            ConfigureMappings();
        }

        private static void ConfigureMappings()
        {
            Mapper.CreateMap<TourDTO, ViewModels.Tour.IndexViewModel>();
            Mapper.CreateMap<Areas.Admin.ViewModels.Tour.CreateViewModel, TourDTO>();
            Mapper.CreateMap<ViewModels.Tour.FilterViewModel, TourFilterDTO>();
            Mapper.CreateMap<TourDTO, ViewModels.Order.PlaceViewModel>()
                .ForMember(dest =>
                    dest.TourTitle, opts =>
                        opts.MapFrom(src =>
                            src.Title));
            Mapper.CreateMap<ViewModels.Order.PlaceViewModel, OrderDTO>();
            Mapper.CreateMap<OrderDTO, ViewModels.Order.IndexViewModel>();
            Mapper.CreateMap<TourDTO, Areas.Admin.ViewModels.Tour.EditViewModel>();
            Mapper.CreateMap<Areas.Admin.ViewModels.Tour.EditViewModel, TourDTO>();
            Mapper.CreateMap<BusinessValueDTO, Areas.Admin.ViewModels.BusinessValues.IndexViewModel>();
            Mapper.CreateMap<Areas.Admin.ViewModels.BusinessValues.SetViewModel, BusinessValueDTO>();
            Mapper.CreateMap<OrderDTO, ViewModels.Order.IndexViewModel>();
            Mapper.CreateMap<OrderDTO, Areas.Admin.ViewModels.Order.IndexViewModel>()
                .ForMember(dest =>
                    dest.UserId, opts =>
                        opts.MapFrom(src =>
                            src.ProfileId));
        }
    }
}
