using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlain.Business.DTO;

namespace TravelPlain.Business.Interfaces
{
    public interface IProfileService : IService
    {
        void Create(ProfileDTO entity);
        ProfileDTO Get(string id);
        IEnumerable<ProfileDTO> GetAll();
    }
}
