using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Request
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Gestion is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimun {2} and maximun {1}", MinimumLength = 3)]
        [Display(Name = "For gestion")]
        public string Gestion { get; set; } //gestion para la que está postulando

        [Required(ErrorMessage = "Degree is required")]
        [StringLength(40, ErrorMessage = "{0} must be: minimun {2} and maximun {1}", MinimumLength = 3)]
        [Display(Name = "Degree")]
        public string Degree { get; set; }

        [StringLength(200, ErrorMessage = "{0} must be: minimun {2} and maximun {1}", MinimumLength = 3)]
        [Display(Name = "Comment")]
        public string? Comment { get; set; }
        public List<Response>? Responses { get; set; }
    }
}
