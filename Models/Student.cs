using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 100 characters")]
        [Display(Name = "Full Name")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Registration Number is required")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Registration Number must be between 5 and 20 characters")]
        [Display(Name = "Registration Number")]
        [RegularExpression(@"^[A-Za-z0-9/\-]+$", ErrorMessage = "Registration Number can only contain letters, numbers, slashes, and hyphens")]
        public string RegistrationNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email Address is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Phone number must be between 10 and 15 digits")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Phone number must contain only digits (10-15) and optional + prefix")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Course is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Course name must be between 2 and 100 characters")]
        public string Course { get; set; } = string.Empty;

        [Required(ErrorMessage = "Year of Study is required")]
        [Range(1, 5, ErrorMessage = "Year of Study must be between 1 and 5")]
        [Display(Name = "Year of Study")]
        public int YearOfStudy { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        [Display(Name = "Gender")]
        public string? Gender { get; set; }

        [Display(Name = "Registration Date")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Last Updated")]
        public DateTime? UpdatedAt { get; set; }
    }
}