using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, ErrorMessage = "Full Name cannot exceed 100 characters")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Registration Number is required")]
        [StringLength(20, ErrorMessage = "Registration Number cannot exceed 20 characters")]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Course is required")]
        [StringLength(100, ErrorMessage = "Course name cannot exceed 100 characters")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year of Study is required")]
        [Range(1, 5, ErrorMessage = "Year of Study must be between 1 and 5")]
        [Display(Name = "Year of Study")]
        public int YearOfStudy { get; set; }
    }
}