using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [StringLength(70, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 5)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Password")]
        public string Password { get; set; }  
        
        public byte RoleID { get; set; }
        public Role? role { get; set; }  
    }
}
