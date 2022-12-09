using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [StringLength(70, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 5)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        public string? Token_Recovery { get; set; }

        public int RoleID { get; set; }
        public Role? role { get; set; }  
    }
}
