using System;
using TravelPlain.Data.EF;
using TravelPlain.Data.Interfaces;
using TravelPlain.Data.Models;
using TravelPlain.Data.Repositories;

namespace TravelPlain.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TravelPlainContext _context;

        private IGenericRepository<Order> orders;
        private IGenericRepository<Tour> tours;
        private IGenericRepository<BusinessValue> businessValues;
        private IProfileRepository profiles;

        public UnitOfWork(string connectionString)
        {
            _context = new TravelPlainContext(connectionString);
        }

        public IGenericRepository<Order> Orders
        {
            get
            {
                if (orders == null)
                {
                    orders = new GenericRepository<Order>(_context);
                }
                return orders;
            }
        }
        public IGenericRepository<Tour> Tours
        {
            get
            {
                if (tours == null)
                {
                    tours = new GenericRepository<Tour>(_context);
                }
                return tours;
            }
        }
        public IGenericRepository<BusinessValue> BusinessValues
        {
            get
            {
                if (businessValues == null)
                {
                    businessValues = new GenericRepository<BusinessValue>(_context);
                }
                return businessValues;
            }
        }
        public IProfileRepository Profiles
        {
            get
            {
                if(profiles == null)
                {
                    profiles = new ProfileRepository(_context);
                }
                return profiles;
            }
        }

        public int SaveChanges() => _context.SaveChanges();
    }
}
