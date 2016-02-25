using System;
using System.ComponentModel.DataAnnotations;
using TravelPlain.Enums;

namespace TravelPlain.Data.Models
{
    public class Order : Entity
    {
        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ProfileId { get; set; }
        public virtual Profile Profile { get; set; }

        public int TourId { get; set; }
        public virtual Tour Tour { get; set; }
    }
}
