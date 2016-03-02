using TravelPlain.Business.Interfaces;
using TravelPlain.Data.Interfaces;
using TravelPlain.Data.Models;

namespace TravelPlain.Business.Services
{
    public abstract class Service : IService
    {
        protected readonly IUnitOfWork _uow;

        public Service(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public int SaveChanges()
        {
            var result = _uow.SaveChanges();
            Log(string.Format("Saving changes. Rows affected: {0}",
                result));
            return result;
        }
        public void Log(string message) => _uow.Log.Add(new LogItem(message));
    }
}
