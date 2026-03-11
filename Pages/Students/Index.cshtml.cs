using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Pages.Students
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Student> Students { get; set; } = new List<Student>();

        [BindProperty(SupportsGet = true)]
        public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CourseFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? YearFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SortOrder { get; set; }

        public List<string> AvailableCourses { get; set; } = new List<string>();

        public int TotalStudents { get; set; }
        public int FilteredCount { get; set; }

        public async Task OnGetAsync()
        {
            // Get all available courses for filter dropdown
            AvailableCourses = await _context.Students
                .Select(s => s.Course)
                .Distinct()
                .OrderBy(c => c)
                .ToListAsync();

            // Start with all students
            var query = _context.Students.AsQueryable();

            // Apply search filter (search in name, registration number, email, course)
            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                var searchLower = SearchTerm.ToLower();
                query = query.Where(s =>
                    s.FullName.ToLower().Contains(searchLower) ||
                    s.RegistrationNumber.ToLower().Contains(searchLower) ||
                    s.Email.ToLower().Contains(searchLower) ||
                    s.Course.ToLower().Contains(searchLower));
            }

            // Apply course filter
            if (!string.IsNullOrWhiteSpace(CourseFilter))
            {
                query = query.Where(s => s.Course == CourseFilter);
            }

            // Apply year filter
            if (YearFilter.HasValue)
            {
                query = query.Where(s => s.YearOfStudy == YearFilter.Value);
            }

            // Apply sorting
            query = SortOrder switch
            {
                "name_desc" => query.OrderByDescending(s => s.FullName),
                "reg_asc" => query.OrderBy(s => s.RegistrationNumber),
                "reg_desc" => query.OrderByDescending(s => s.RegistrationNumber),
                "course_asc" => query.OrderBy(s => s.Course),
                "course_desc" => query.OrderByDescending(s => s.Course),
                "year_asc" => query.OrderBy(s => s.YearOfStudy),
                "year_desc" => query.OrderByDescending(s => s.YearOfStudy),
                "date_asc" => query.OrderBy(s => s.CreatedAt),
                "date_desc" => query.OrderByDescending(s => s.CreatedAt),
                _ => query.OrderBy(s => s.FullName) // default: name_asc
            };

            TotalStudents = await _context.Students.CountAsync();
            Students = await query.ToListAsync();
            FilteredCount = Students.Count;
        }

        public IActionResult OnPostClearFilters()
        {
            return RedirectToPage();
        }
    }
}