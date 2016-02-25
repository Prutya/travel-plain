using System.ComponentModel.DataAnnotations;

namespace TravelPlain.Data.Models
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
    }
}
