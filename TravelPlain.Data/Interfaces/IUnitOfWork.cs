using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlain.Data.Models;

namespace TravelPlain.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<Order> Orders { get; }
        IGenericRepository<Tour> Tours { get; }
        IGenericRepository<BusinessValue> BusinessValues { get; }
        IProfileRepository Profiles { get; }

        int SaveChanges();
    }
}
