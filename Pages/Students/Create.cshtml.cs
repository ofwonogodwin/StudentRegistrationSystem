using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Pages.Students
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; } = new Student();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if registration number already exists
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.RegistrationNumber == Student.RegistrationNumber);

            if (existingStudent != null)
            {
                ModelState.AddModelError("Student.RegistrationNumber",
                    "A student with this registration number already exists.");
                return Page();
            }

            // Check if email already exists
            var existingEmail = await _context.Students
                .FirstOrDefaultAsync(s => s.Email == Student.Email);

            if (existingEmail != null)
            {
                ModelState.AddModelError("Student.Email",
                    "A student with this email address already exists.");
                return Page();
            }

            Student.CreatedAt = DateTime.Now;
            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Student {Student.FullName} has been registered successfully!";
            return RedirectToPage("./Index");
        }
    }
}