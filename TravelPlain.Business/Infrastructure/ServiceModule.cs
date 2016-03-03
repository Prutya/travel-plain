using AutoMapper;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlain.Business.DTO;
using TravelPlain.Data;
using TravelPlain.Data.Interfaces;
using TravelPlain.Data.Models;

namespace TravelPlain.Business.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>()
                .To<UnitOfWork>()
                .WithConstructorArgument(connectionString);

            ConfigureMappings();
        }
        /// <summary>
        /// Automapper configuration
        /// </summary>
        protected void ConfigureMappings()
        {
            Mapper.CreateMap<Tour, TourDTO>();
            Mapper.CreateMap<TourDTO, Tour>();

            Mapper.CreateMap<Data.Models.Profile, ProfileDTO>();
            Mapper.CreateMap<ProfileDTO, Data.Models.Profile>();

            Mapper.CreateMap<Data.Models.Order, OrderDTO>()
                .ForMember(dest =>
                    dest.TourTitle, opts =>
                        opts.MapFrom(src =>
                            src.Tour.Title));
            Mapper.CreateMap<OrderDTO, Data.Models.Order>();

            Mapper.CreateMap<BusinessValueDTO, BusinessValue>();
            Mapper.CreateMap<BusinessValue, BusinessValueDTO>();
            Mapper.CreateMap<LogItem, LogItemDTO>();
        }
    }
}
