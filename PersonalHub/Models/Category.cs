using System.ComponentModel.DataAnnotations;

namespace PersonalHub.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        // add nullable child ref to Product model
        public List<Schedule>? Schedules { get; set; }
    }
}
