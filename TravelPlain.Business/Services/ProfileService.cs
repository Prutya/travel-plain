using AutoMapper;
using System;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Interfaces;
using TravelPlain.Data.Interfaces;

namespace TravelPlain.Business.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _uow;

        public ProfileService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public ProfileDTO Get(string id)
        {
            var profile = _uow.Profiles.Get(id);
            var profileDto = Mapper.Map<ProfileDTO>(profile);

            return profileDto;
        }

        public void Create(ProfileDTO entity)
        {
            var profile = Mapper.Map<Data.Models.Profile>(entity);
            _uow.Profiles.Add(profile);
        }

        public int SaveChanges() => _uow.SaveChanges();
    }
}
