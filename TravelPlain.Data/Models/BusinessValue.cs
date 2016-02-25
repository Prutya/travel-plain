using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPlain.Data.Models
{
    public class BusinessValue : Entity
    {
        [Required]
        [Range(0,100)]
        public byte DiscountPercentCap { get; set; }

        [Required]
        [Range(0,100)]
        public byte DiscountPercentIncrement { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime TimeSet { get; set; }

        [Required]
        public bool IsValid { get; set; }
    }
}
