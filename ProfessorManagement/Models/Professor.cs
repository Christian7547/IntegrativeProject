using System.ComponentModel.DataAnnotations;


namespace ProfessorManagement.Models
{
    public class Professor
    {
        [Key]
        public byte Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "SecondLastName is required")]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "SecondLastName")]
        public string SecondLastName { get; set; }
        [Required(ErrorMessage = "CI is required")]
        [StringLength(15, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 7)]
        [Display(Name = "CI")]
        public string CI { get; set; }

        [Required(ErrorMessage = "BirthDate date is requeride")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [StringLength(12, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 7)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        [StringLength(150, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "RegisterDate date is requeride")]
        [DataType(DataType.Date)]
        public DateTime RegisterDate { get; set; }
        [Required(ErrorMessage = "RegisterType is required")]
        [Display(Name = "RegisterType")]
        public byte RegisterType { get; set; }
        [Required(ErrorMessage = "Status is required")]
        [Display(Name = "Status")]
        public byte Status { get; set; }
    }
}
