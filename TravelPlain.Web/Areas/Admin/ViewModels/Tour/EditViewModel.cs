using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TravelPlain.Enums;

namespace TravelPlain.Web.Areas.Admin.ViewModels.Tour
{
    public class EditViewModel
    {
        [Required]
        [UIHint("Hidden")]
        public int Id { get; set; }

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
        [Range(typeof(decimal), "00.01", "1000000000000", ErrorMessage = "The lowest valid price is 00,01, use dot to separate the fraction.")]
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