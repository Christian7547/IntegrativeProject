using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Response
    {
        [Key]
        public int ResponseId { get; set; }

        [Required(ErrorMessage = "Please enter a description for the answer")]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public byte NewStatusRequest { get; set; }
        [Required]
        public int RequestId { get; set; }
        public Request Request { get; set; }
    }
}
