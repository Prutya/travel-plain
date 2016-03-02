using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelPlain.Web.Areas.Admin.ViewModels.User
{
    public class IndexViewModel
    {
        public string Id { get; set; }
        
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Lockout")]
        public DateTime LockedOutTill { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}