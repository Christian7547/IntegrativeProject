using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class SendRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string NamesProfessor { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        [Display(Name = "Last name")]
        public string LastNameProfessor { get; set; }

        [Required(ErrorMessage = "Second lastname is required")]
        [Display(Name = "Second last name")]
        public string? SecondLastNameProfessor { get; set; }

        [Required(ErrorMessage = "CI is required")]
        [Display(Name = "C.I")]
        public string CIProfessor { get; set; }

        [Required(ErrorMessage = "Birthdate is required")]
        [Display(Name = "Birth Date")]
        public DateTime BirthDateProfessor { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        [Display(Name = "Phone")]
        public int PhoneProfessor { get; set; }

        [Required(ErrorMessage = "Adress is required")]
        [Display(Name = "Address")]
        public string AddressProfessor { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Display(Name = "For what year?")]
        public string ForWhatGestion { get; set; }

        [Required(ErrorMessage = "Degree is required")]
        public string Degree { get; set; } //titulacion

        public string? Comment { get; set; }
    }
}
