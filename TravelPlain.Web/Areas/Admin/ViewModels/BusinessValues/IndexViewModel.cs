using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPlain.Web.Areas.Admin.ViewModels.BusinessValues
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Discount cap")]
        public byte DiscountPercentCap { get; set; }

        [Display(Name = "Discount increment per order")]
        public byte DiscountPercentIncrement { get; set; }

        [Display(Name = "Time set")]
        public DateTime TimeSet { get; set; }
    }
}