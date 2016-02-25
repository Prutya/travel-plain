using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelPlain.Data.Models
{
    public class Profile
    {
        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
