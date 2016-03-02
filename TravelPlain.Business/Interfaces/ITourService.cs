using System.Collections.Generic;
using TravelPlain.Business.DTO;

namespace TravelPlain.Business.Interfaces
{
    public interface ITourService : IService
    {
        IEnumerable<TourDTO> Get(TourFilterDTO filter = null);
        TourDTO GetById(int id);
        void Create(TourDTO entity);
        void Update(TourDTO entity);
        void ToggleHot(int id);
    }
}
