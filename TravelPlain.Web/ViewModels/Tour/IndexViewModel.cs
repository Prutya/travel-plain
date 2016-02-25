using System.ComponentModel.DataAnnotations;
using TravelPlain.Enums;

namespace TravelPlain.Web.ViewModels.Tour
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "People number")]
        public byte PeopleNumber { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Hot")]
        public bool IsHot { get; set; }
        
        [Display(Name = "Tour type")]
        public TourType TourType { get; set; }
        
        [Display(Name = "Hotel type")]
        public HotelType HotelType { get; set; }
        
        [Display(Name = "Transfer type")]
        public TransferType TransferType { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }
    }
}