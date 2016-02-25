using System.ComponentModel.DataAnnotations;
using TravelPlain.Enums;

namespace TravelPlain.Web.ViewModels.Tour
{
    public class FilterViewModel
    {
        [Required]
        [Display(Name = "Display unavailable tours")]
        public bool DisplayUnavailable { get; set; } = false;

        [Display(Name = "Min price")]
        [Range(typeof(decimal), "00.01", "1000000000.00", ErrorMessage = "The lowest valid price is 00,01, use dot to separate the fraction.")]
        public decimal? MinPrice { get; set; }

        [Display(Name = "Max price")]
        [Range(typeof(decimal), "00.01", "1000000000.00", ErrorMessage = "The lowest valid price is 00,01, use dot to separate the fraction.")]
        public decimal? MaxPrice { get; set; }

        [Display(Name = "Min people number")]
        [Range(1, 255, ErrorMessage = "People number may range from 1 to 50.")]
        public byte? MinPeople { get; set; }

        [Display(Name = "Max people number")]
        [Range(1, 255, ErrorMessage = "People number may range from 1 to 50.")]
        public byte? MaxPeople { get; set; }

        [Display(Name = "Tour type")]
        public TourType? TourType { get; set; }

        [Display(Name = "Hotel type")]
        public HotelType? HotelType { get; set; }

        [Display(Name = "Transfer type")]
        public TransferType? TransferType { get; set; }
    }
}