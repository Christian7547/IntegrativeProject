using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Role name:")]
        public string RoleName { get; set; }
        
        public ICollection<User>? Users { get; set; }   
    }
}
