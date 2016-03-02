using AutoMapper;
using System;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Interfaces;
using TravelPlain.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace TravelPlain.Business.Services
{
    public class ProfileService : Service, IProfileService
    {
        public ProfileService(IUnitOfWork uow)
            : base(uow)
        { }

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
            Log(string.Format("Created new profile [Id:{0}, FirstName:{1}, LastName:{2}].",
                profile.Id,
                profile.FirstName,
                profile.LastName
                ));
        }

        public IEnumerable<ProfileDTO> GetAll() =>
            _uow.Profiles.Get()
                .ToList()
                .Select(o => Mapper.Map<ProfileDTO>(o));
    }
}
