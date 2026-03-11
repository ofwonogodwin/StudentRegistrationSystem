using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();

        public string? ErrorMessage { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "Full Name is required")]
            [StringLength(100, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 100 characters")]
            [Display(Name = "Full Name")]
            public string FullName { get; set; } = string.Empty;

            [Required(ErrorMessage = "Username is required")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 50 characters")]
            [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username can only contain letters, numbers, and underscores")]
            [Display(Name = "Username")]
            public string Username { get; set; } = string.Empty;

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            [Display(Name = "Email")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "Password is required")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = string.Empty;

            [Required(ErrorMessage = "Please confirm your password")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "Passwords do not match")]
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public IActionResult OnGet()
        {
            // If already authenticated, redirect to students page
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToPage("/Students/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == Input.Username))
            {
                ErrorMessage = "Username is already taken. Please choose a different one.";
                return Page();
            }

            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == Input.Email))
            {
                ErrorMessage = "An account with this email already exists.";
                return Page();
            }

            // Create new user
            var user = new User
            {
                Username = Input.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(Input.Password),
                FullName = Input.FullName,
                Email = Input.Email,
                Role = "User",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Account created successfully! Please sign in with your credentials.";
            return RedirectToPage("/Account/Login");
        }
    }
}
