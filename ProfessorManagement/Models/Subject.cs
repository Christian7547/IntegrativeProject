﻿using System.ComponentModel.DataAnnotations;

namespace ProfessorManagement.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "{0} must be: minimum {2} and maximum {1}", MinimumLength = 3)]
        [Display(Name = "Subject")]
        public string Name { get; set; }
        [Required]
        public string Area { get; set; }
    }
}
