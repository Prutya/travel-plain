using TravelPlain.Enums;

namespace TravelPlain.Business.DTO
{
    public class TourFilterDTO
    {
        public bool DisplayUnavailable { get; set; } = false;
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public byte? MinPeople { get; set; }
        public byte? MaxPeople { get; set; }
        public TourType? TourType { get; set; }
        public HotelType? HotelType { get; set; }
        public TransferType? TransferType { get; set; }
    }
}
