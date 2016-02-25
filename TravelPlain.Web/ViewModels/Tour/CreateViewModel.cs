using System.ComponentModel.DataAnnotations;
using TravelPlain.Enums;

namespace TravelPlain.Web.ViewModels.Tour
{
    public class CreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(400)]
        public string Description { get; set; }

        [Required]
        [Range(1, 255)]
        [Display(Name = "People number")]
        public byte PeopleNumber { get; set; } = 2;

        [Required]
        [Range(typeof(decimal), "00.01", "1000000000000")]
        public decimal Price { get; set; }

        [Required]
        [Display(Name = "Available tour")]
        public bool IsAvailable { get; set; } = true;

        [Required]
        [Display(Name = "Hot tour")]
        public bool IsHot { get; set; }

        [Required]
        [Display(Name = "Tour type")]
        public TourType TourType { get; set; }

        [Required]
        [Display(Name = "Hotel type")]
        public HotelType HotelType { get; set; }

        [Required]
        [Display(Name = "Transfer type")]
        public TransferType TransferType { get; set; }
    }
}