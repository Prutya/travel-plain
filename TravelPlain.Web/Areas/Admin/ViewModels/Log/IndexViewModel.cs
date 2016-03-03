using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPlain.Web.Areas.Admin.ViewModels.Log
{
    public class IndexViewModel
    {
        public int Id { get; set; }

        public DateTime Time { get; set; }
        
        [Display(Name = "Event")]
        public string Message { get; set; }
    }
}