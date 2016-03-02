using System;
using System.ComponentModel.DataAnnotations;

namespace TravelPlain.Data.Models
{
    public class LogItem : Entity
    {
        public LogItem(string message)
        {
            Time = DateTimeOffset.Now.UtcDateTime;
            Message = message;
        }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        [StringLength(1024)]
        public string Message { get; set; }
    }
}
