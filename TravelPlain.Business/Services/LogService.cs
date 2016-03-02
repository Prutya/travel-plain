using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using TravelPlain.Business.DTO;
using TravelPlain.Business.Interfaces;
using TravelPlain.Data.Interfaces;

namespace TravelPlain.Business.Services
{
    public class LogService : Service, ILogService
    {
        public LogService(IUnitOfWork uow)
            : base(uow)
        { }

        public IEnumerable<LogItemDTO> GetAll() =>
            _uow.Log.Get()
                .Select(o => Mapper.Map<LogItemDTO>(o));

        public LogItemDTO GetById(int id) =>
            Mapper.Map<LogItemDTO>(_uow.Log.Get(id));
    }
}
