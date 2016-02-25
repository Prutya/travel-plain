using System.Collections.Generic;
using System.Linq;
using TravelPlain.Data.Models;

namespace TravelPlain.Data.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> Get();
        TEntity Get(int id);
        TEntity Add(TEntity entity);
        TEntity Remove(TEntity entity);
        int SaveChanges();
    }
}
