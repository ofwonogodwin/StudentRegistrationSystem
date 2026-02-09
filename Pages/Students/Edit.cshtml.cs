using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; } = new Student();

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            Student = student;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if registration number already exists for a different student
            var existingStudent = await _context.Students
                .FirstOrDefaultAsync(s => s.RegistrationNumber == Student.RegistrationNumber && s.Id != Student.Id);

            if (existingStudent != null)
            {
                ModelState.AddModelError("Student.RegistrationNumber",
                    "A student with this registration number already exists.");
                return Page();
            }

            _context.Attach(Student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Student {Student.FullName} has been updated successfully!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(Student.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}