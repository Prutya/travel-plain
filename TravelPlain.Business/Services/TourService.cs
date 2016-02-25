﻿using AutoMapper;
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
    public class TourService : ITourService
    {
        private readonly IUnitOfWork _uow;

        public TourService(IUnitOfWork uow)
        {
            _uow = uow;
        }

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

        public void Create(TourDTO tourDto)
        {
            var tour = Mapper.Map<Tour>(tourDto);
            try
            {
                _uow.Tours.Add(tour);
            }
            catch (Exception ex)
            {
                throw new ValidationException("Exception \"" + ex.Message + "\" at " + ex.Source);
            }
        }

        public TourDTO GetById(int id) => Mapper.Map<TourDTO>(_uow.Tours.Get(id));

        public int SaveChanges() => _uow.SaveChanges();
    }
}