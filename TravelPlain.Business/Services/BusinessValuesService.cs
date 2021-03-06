﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Interfaces;
using TravelPlain.Data.Interfaces;
using TravelPlain.Data.Models;

namespace TravelPlain.Business.Services
{
    public class BusinessValuesService : Service, IBusinessValuesService
    {
        public BusinessValuesService(IUnitOfWork uow)
            : base(uow)
        { }

        public IEnumerable<BusinessValueDTO> GetAll() => 
            _uow.BusinessValues.Get()
                .ToList()
                .Select(o => Mapper.Map<BusinessValueDTO>(o));

        public BusinessValueDTO GetLast() => 
            Mapper.Map<BusinessValueDTO>(
            _uow.BusinessValues.Get()
                .OrderByDescending(o => o.TimeSet)
                .FirstOrDefault());

        public void Create(BusinessValueDTO entity)
        {
            var businessValue = Mapper.Map<BusinessValue>(entity);
            businessValue.TimeSet = DateTime.Now;
            _uow.BusinessValues.Add(businessValue);
            Log(string.Format("Creating new business values [DiscountPercentCap:{0}, DiscountPercentIncrement:{1}].",
                businessValue.DiscountPercentCap, businessValue.DiscountPercentIncrement));            
        }
    }
}
