using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Services.Interfaces;

namespace StudentRegistrationSystem.Pages.Students
{
    [Authorize(Policy = "StaffOrAdmin")]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuditLogService _auditLogService;

        public DeleteModel(ApplicationDbContext context, IAuditLogService auditLogService)
        {
            _context = context;
            _auditLogService = auditLogService;
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
                await _auditLogService.RecordAsync(
                    "DELETE",
                    "Student",
                    Student.Id.ToString(),
                    $"Deleted student record for {Student.FullName}",
                    User.Identity?.Name ?? "Unknown",
                    User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value ?? "Unknown",
                    HttpContext.Connection.RemoteIpAddress?.ToString());
                TempData["SuccessMessage"] = $"Student {Student.FullName} has been deleted successfully!";
            }

            return RedirectToPage("./Index");
        }
    }
}