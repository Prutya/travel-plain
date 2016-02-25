using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlain.Data.Models;

namespace TravelPlain.Data.Interfaces
{
    public interface IProfileRepository
    {
        IQueryable<Profile> Get();
        Profile Get(string id);
        Profile Add(Profile entity);
        Profile Remove(Profile entity);
        int SaveChanges();
    }
}
