using System;
using System.ComponentModel.DataAnnotations;
using TravelPlain.Enums;

namespace TravelPlain.Web.Areas.Admin.ViewModels.Order
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Tour title")]
        public string TourTitle { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Time { get; set; }
        public decimal Price { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserId { get; set; }
    }
}