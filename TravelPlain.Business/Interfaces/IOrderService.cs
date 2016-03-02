using System.Collections.Generic;
using TravelPlain.Business.DTO;
using TravelPlain.Enums;

namespace TravelPlain.Business.Interfaces
{
    public interface IOrderService : IService
    {
        void Place(OrderDTO data);
        void Cancel(int orderId, string profileId);
        void ChangeOrderStatus(int orderId, OrderStatus status);
        IEnumerable<OrderDTO> GetByProfileId(string profileId);
        IEnumerable<OrderDTO> GetAll();
        OrderDTO GetById(int id);
        decimal CalculatePrice(int tourId, string userId);
    }
}
