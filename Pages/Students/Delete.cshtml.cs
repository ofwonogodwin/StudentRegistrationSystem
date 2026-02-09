using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                Student = student;
                _context.Students.Remove(Student);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Student {Student.FullName} has been deleted successfully!";
            }

            return RedirectToPage("./Index");
        }
    }
}