using System.ComponentModel.DataAnnotations;

namespace PersonalHub.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        
        [MaxLength(255)]
        public string? Description { get; set; }

        public string? Location { get; set; }

        // add nullable child ref to Product model
        public List<Schedule>? Schedules { get; set; }
    }
}
