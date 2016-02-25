using System.ComponentModel.DataAnnotations;

namespace TravelPlain.Web.ViewModels.Order
{
    public class PlaceViewModel
    {
        [Required]
        [UIHint("Hidden")]
        public int TourId { get; set; }

        [Display(Name = "Tour title")]
        public string TourTitle { get; set; }

        [Display(Name = "You save")]
        public decimal YouSave { get; set; }

        public decimal Price { get; set; }
    }
}