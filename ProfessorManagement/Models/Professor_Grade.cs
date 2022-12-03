using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Professor_Grade
    {
        [Key]
        public int Id { get; set; }

        public int ProfessorId { get; set; }
        public int GradeId { get; set; }

        public Professor professor { get; set; }
        public Grade grade { get; set; }
    }
}
