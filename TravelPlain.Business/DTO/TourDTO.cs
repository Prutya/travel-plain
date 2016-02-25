using System.Collections.Generic;
using TravelPlain.Enums;

namespace TravelPlain.Business.DTO
{
    public class TourDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte PeopleNumber { get; set; }
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsHot { get; set; }
        public TourType TourType { get; set; }
        public HotelType HotelType { get; set; }
        public TransferType TransferType { get; set; }
        public string ImageName { get; set; }
    }
}
