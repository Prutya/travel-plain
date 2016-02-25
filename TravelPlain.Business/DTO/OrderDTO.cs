using System;
using TravelPlain.Enums;

namespace TravelPlain.Business.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }

        public string ProfileId { get; set; }

        public string TourTitle { get; set; }
        public int TourId { get; set; }
    }
}
