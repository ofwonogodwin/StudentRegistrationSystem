using System.ComponentModel.DataAnnotations;

namespace StudentRegistrationSystem.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [StringLength(20)]
        public string Role { get; set; } = "User"; // Admin, User

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? LastLogin { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
