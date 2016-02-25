using TravelPlain.Data.Models;
using TravelPlain.Data.Interfaces;
using System;
using System.Linq;
using System.Data.Entity;
using TravelPlain.Data.EF;

namespace TravelPlain.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        protected TravelPlainContext _context;
        protected DbSet<TEntity> _dbSet;

        public GenericRepository(TravelPlainContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> Get() => _dbSet.AsQueryable();

        public TEntity Get(int id) => _dbSet.Find(id);

        public TEntity Add(TEntity entity) => _dbSet.Add(entity);

        public TEntity Remove(TEntity entity) => _dbSet.Remove(entity);

        public int SaveChanges() => _context.SaveChanges();
    }
}
