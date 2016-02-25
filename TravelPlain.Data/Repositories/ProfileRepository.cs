using System;
using System.Linq;
using TravelPlain.Data.EF;
using TravelPlain.Data.Interfaces;
using TravelPlain.Data.Models;

namespace TravelPlain.Data.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        protected TravelPlainContext _context;

        public ProfileRepository(TravelPlainContext context)
        {
            _context = context;
        }

        public IQueryable<Profile> Get() => _context.Profiles.AsQueryable();

        public Profile Get(string id) => _context.Profiles.Find(id);

        public Profile Add(Profile entity) => _context.Profiles.Add(entity);

        public Profile Remove(Profile entity) => _context.Profiles.Remove(entity);

        public int SaveChanges() => _context.SaveChanges();
    }
}
