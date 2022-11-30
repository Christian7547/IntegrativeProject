using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Professor_Subject
    {
        [Key]
        public int Id { get; set; }

        public byte ProfessorId { get; set; }
        public byte SubjectId { get; set; }

        public Professor professor { get; set; }
        public Subject subject { get; set; }
    }
}
