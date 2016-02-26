using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPlain.Web.Areas.Admin.ViewModels.BusinessValues
{
    public class SetViewModel
    {
        [Required]
        [Display(Name = "Discount cap")]
        [Range(0,100)]
        public byte DiscountPercentCap { get; set; }

        [Required]
        [Range(0,100)]
        [Display(Name = "Discount increment per order")]
        public byte DiscountPercentIncrement { get; set; }
    }
}