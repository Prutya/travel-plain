using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TravelPlain.Web.Areas.Admin.ViewModels.User
{
    public class AddToRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        [Required]
        public string Role { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}