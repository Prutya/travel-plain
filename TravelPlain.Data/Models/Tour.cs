using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelPlain.Enums;

namespace TravelPlain.Data.Models
{
    public class Tour : Entity
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(400)]
        public string Description { get; set; }

        [Required]
        [Range(1, 255)]
        public byte PeopleNumber { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public bool IsHot { get; set; }

        [Required]
        public TourType TourType { get; set; }

        [Required]
        public HotelType HotelType { get; set; }

        [Required]
        public TransferType TransferType { get; set; }

        [StringLength(200)]
        public string ImageName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
