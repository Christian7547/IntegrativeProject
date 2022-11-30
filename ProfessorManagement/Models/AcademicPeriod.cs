using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class AcademicPeriod
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Description")]
        [Required]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FinishDate { get; set; }    
    }
}
