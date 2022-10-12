using System.ComponentModel.DataAnnotations;

namespace PersonalHub.Models
{
    public class Schedule
    {

        public int ScheduleId { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Title { get; set; }
        
        [MaxLength(255)]
        public string? Description { get; set; }
        public string? Location { get; set; }
        
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime Time { get; set; }

        [Required]
        public string? Day { get; set; }
        [Required]
        public string? Month { get; set; }
        [Required]
        public string? Year { get; set; }

        // FK for Parent Category
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        // Parent ref for auto-joins
        public Category? Category { get; set; }
    }
}
