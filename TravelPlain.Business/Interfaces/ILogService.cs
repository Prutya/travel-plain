using System.Collections.Generic;
using TravelPlain.Business.DTO;

namespace TravelPlain.Business.Interfaces
{
    public interface ILogService : IService
    {
        LogItemDTO GetById(int id);
        IEnumerable<LogItemDTO> GetAll();
    }
}
