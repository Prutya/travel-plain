using System;

namespace TravelPlain.Business.DTO
{
    public class BusinessValueDTO
    {
        public int Id { get; set; }
        public byte DiscountPercentCap { get; set; }
        public byte DiscountPercentIncrement { get; set; }
        public DateTime TimeSet { get; set; }
    }
}
