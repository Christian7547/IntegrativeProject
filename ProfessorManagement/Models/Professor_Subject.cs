using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Professor_Subject
    {
        [Key]
        public int Id { get; set; }

        public int ProfessorId { get; set; }
        public int SubjectId { get; set; }

        public Professor professor { get; set; }
        public Subject subject { get; set; }
    }
}
