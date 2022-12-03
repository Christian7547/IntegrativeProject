using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class AcademicDesignation
    {
        [Key]
        public int Id { get; set; } 

        public int ProfessorId { get; set; }

        public int AcademicPeriodId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DesignationDate { get; set; }

        public Professor Professor { get; set; }    

        public AcademicPeriod AcademicPeriod { get; set; }
    }
}
