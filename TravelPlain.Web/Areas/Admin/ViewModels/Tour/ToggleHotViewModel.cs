using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPlain.Web.Areas.Admin.ViewModels.Tour
{
    public class ToggleHotViewModel
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "People number")]
        public byte PeopleNumber { get; set; }

        public decimal Price { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Hot")]
        public bool IsHot { get; set; }

        [Display(Name = "Image")]
        public string ImageName { get; set; }
    }
}