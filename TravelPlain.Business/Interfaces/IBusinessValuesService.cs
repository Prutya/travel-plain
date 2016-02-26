using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlain.Business.DTO;

namespace TravelPlain.Business.Interfaces
{
    public interface IBusinessValuesService : IService
    {
        IEnumerable<BusinessValueDTO> GetAll();
        BusinessValueDTO GetLast();
        void Create(BusinessValueDTO entity);
    }
}
