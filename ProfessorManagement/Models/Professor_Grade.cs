using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Professor_Grade
    {
        [Key]
        public int Id { get; set; }

        public byte ProfessorId { get; set; }
        public byte GradeId { get; set; }

        public Professor professor { get; set; }
        public Grade grade { get; set; }
    }
}
