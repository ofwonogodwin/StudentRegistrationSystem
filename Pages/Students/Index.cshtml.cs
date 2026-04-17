using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Models.ViewModels;
using StudentRegistrationSystem.Services.Interfaces;

namespace StudentRegistrationSystem.Pages.Students
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;

        public IndexModel(IStudentService studentService)
        {
            _studentService = studentService;
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

        [BindProperty(SupportsGet = true)]
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public List<string> AvailableCourses { get; set; } = new List<string>();

        public int TotalStudents { get; set; }
        public int FilteredCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }

        public async Task OnGetAsync()
        {
            AvailableCourses = (await _studentService.GetAvailableCoursesAsync()).ToList();

            var pagedResult = await _studentService.GetStudentsAsync(SearchTerm, CourseFilter, YearFilter, SortOrder, PageIndex, PageSize);
            Students = pagedResult.Items.ToList();
            TotalStudents = pagedResult.TotalCount;
            FilteredCount = pagedResult.TotalCount;
            TotalPages = pagedResult.TotalPages;
            HasPreviousPage = pagedResult.HasPreviousPage;
            HasNextPage = pagedResult.HasNextPage;
        }

        public async Task<IActionResult> OnGetExportCsvAsync()
        {
            if (!(User.IsInRole("Admin") || User.IsInRole("Staff")))
            {
                return Forbid();
            }

            var csv = await _studentService.ExportStudentsCsvAsync(SearchTerm, CourseFilter, YearFilter, SortOrder);
            return File(csv, "text/csv", $"students-{DateTime.Now:yyyyMMdd-HHmmss}.csv");
        }
    }
}