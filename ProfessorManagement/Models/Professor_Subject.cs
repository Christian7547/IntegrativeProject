using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Professor_Subject
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Professor")]
        public int ProfessorId { get; set; }
        [Display(Name = "Subject")]
        public int SubjectId { get; set; }
        [Display(Name = "Professor")]
        public Professor professor { get; set; }
        [Display(Name = "Subject")]
        public Subject subject { get; set; }
    }
}
