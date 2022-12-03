using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Professor_Request
    {
        [Key]
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime ChangeDate { get; set; }
        [Required]
        public byte NewStatus { get; set; }
        public int ProfessorId { get; set; }
        public int RequestId { get; set; }  
        public Professor Professor { get; set; }
        public Request Request { get; set; }
    }
}
