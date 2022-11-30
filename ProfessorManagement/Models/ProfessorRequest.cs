using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class ProfessorRequest
    {
        [Key]
        public int Id { get; set; }

        public byte ProfessorId { get; set; }

        public int RequestId { get; set; }

        [DataType(DataType.Date)]
        public DateTime ChangeDate { get; set; }

        public byte NewStatus { get; set; }
    }
}
