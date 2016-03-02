using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Infrastructure;
using TravelPlain.Business.Interfaces;
using TravelPlain.Data.Interfaces;
using TravelPlain.Data.Models;
using TravelPlain.Enums;

namespace TravelPlain.Business.Services
{
    public class TourService : Service, ITourService
    {
        public TourService(IUnitOfWork uow)
            : base(uow)
        { }

        public IEnumerable<TourDTO> Get(TourFilterDTO filter = null)
        {
            var tours = _uow.Tours.Get();

            if (filter != null)
            {
                if (!filter.DisplayUnavailable)
                {
                    tours = tours.Where(o => o.IsAvailable);
                }

                if (filter.MinPrice != null)
                {
                    tours = tours.Where(o => o.Price >= filter.MinPrice.Value);
                }

                if (filter.MaxPrice != null)
                {
                    tours = tours.Where(o => o.Price <= filter.MaxPrice.Value);
                }

                if (filter.MinPeople != null)
                {
                    tours = tours.Where(o => o.PeopleNumber >= filter.MinPeople.Value);
                }

                if (filter.MaxPeople != null)
                {
                    tours = tours.Where(o => o.PeopleNumber <= filter.MaxPeople.Value);
                }

                if (filter.TourType != null)
                {
                    tours = tours.Where(o => o.TourType == filter.TourType.Value);
                }

                if (filter.HotelType != null)
                {
                    tours = tours.Where(o => o.HotelType == filter.HotelType.Value);
                }

                if (filter.TransferType != null)
                {
                    tours = tours.Where(o => o.TransferType == filter.TransferType.Value);
                }
            }

            var toursDtos = tours
                .ToList()
                .Select(o => Mapper.Map<TourDTO>(o));

            return toursDtos;
        }

        public void Create(TourDTO entity)
        {
            var tour = _uow.Tours.Add(Mapper.Map<Tour>(entity));
            Log(string.Format("Created new tour [Id:{0}, Title:{1}, Description:{2}, PeopleNumber:{3}, Price:{4}, IsAvailable:{5}, IsHot:{6}, TourType:{7}, HotelType:{8}, TranfserType:{9}, ImageName:{10}].",
                tour.Id,
                tour.Title,
                tour.Description.Substring(0, 50) + "...",
                tour.PeopleNumber,
                tour.Price,
                tour.IsAvailable,
                tour.IsHot,
                tour.TourType,
                tour.HotelType,
                tour.TransferType,
                tour.ImageName
                ));
        }

        public void Update(TourDTO entity)
        {
            var tour = _uow.Tours.Get(entity.Id);

            if (tour != null)
            {
                //TODO: use AutoMapper here?
                tour.Title = entity.Title;
                tour.Description = entity.Description;
                tour.Price = entity.Price;
                tour.PeopleNumber = entity.PeopleNumber;
                tour.IsHot = entity.IsHot;
                tour.IsAvailable = entity.IsAvailable;
                tour.TourType = entity.TourType;
                tour.HotelType = entity.HotelType;
                tour.TransferType = entity.TransferType;

                if (entity.ImageName != null)
                {
                    tour.ImageName = entity.ImageName;
                }

                Log(string.Format("Updating tour info [Id:{0}].",
                    tour.Id
                ));
            }
            else
            {
                throw new ValidationException("Tour does not exist", string.Empty);
            }
        }

        public TourDTO GetById(int id) => Mapper.Map<TourDTO>(_uow.Tours.Get(id));

        public void ToggleHot(int id)
        {
            var tour = _uow.Tours.Get()
                .FirstOrDefault(o => o.Id == id);
            if (tour == null)
            {
                throw new ValidationException("Tour does not exist", "");
            }
            tour.IsHot = !tour.IsHot;
            Log(string.Format("Updating tour [Id:{0}, IsHot:{1}].",
                    tour.Id,
                    tour.IsHot
                ));
        }
    }
}
