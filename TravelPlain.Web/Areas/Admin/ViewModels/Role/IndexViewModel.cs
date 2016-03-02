using System.ComponentModel.DataAnnotations;

namespace TravelPlain.Web.Areas.Admin.ViewModels.Role
{
    public class IndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Role name")]
        public string Name { get; set; }
    }
}