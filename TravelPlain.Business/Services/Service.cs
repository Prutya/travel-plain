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
            Log(string.Format("Saving changes."));
            return _uow.SaveChanges();
        }
        public void Log(string message)
        {
            _uow.Log.Add(new LogItem(message));
            _uow.SaveChanges();
        }
    }
}
