using System.Collections.Generic;
using TravelPlain.Business.DTO;

namespace TravelPlain.Business.Interfaces
{
    public interface IOrderService : IService
    {
        void Place(OrderDTO data);
        void Cancel(int orderId, string profileId);
        IEnumerable<OrderDTO> GetByProfileId(string profileId);
        OrderDTO GetById(int id);
        decimal CalculatePrice(int tourId, string userId);
    }
}
