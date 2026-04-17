using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRegistrationSystem.Models.ViewModels;
using StudentRegistrationSystem.Services.Interfaces;

namespace StudentRegistrationSystem.Pages
{
    [Authorize(Policy = "StaffOrAdmin")]
    public class DashboardModel : PageModel
    {
        private readonly IDashboardService _dashboardService;

        public DashboardModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public DashboardSummaryViewModel Summary { get; set; } = new();
        public string CourseLabelsJson { get; set; } = "[]";
        public string CourseValuesJson { get; set; } = "[]";
        public string YearLabelsJson { get; set; } = "[]";
        public string YearValuesJson { get; set; } = "[]";

        public async Task OnGetAsync()
        {
            Summary = await _dashboardService.GetSummaryAsync();

            CourseLabelsJson = JsonSerializer.Serialize(Summary.StudentsByCourse.Select(point => point.Label));
            CourseValuesJson = JsonSerializer.Serialize(Summary.StudentsByCourse.Select(point => point.Value));
            YearLabelsJson = JsonSerializer.Serialize(Summary.StudentsByYear.Select(point => point.Label));
            YearValuesJson = JsonSerializer.Serialize(Summary.StudentsByYear.Select(point => point.Value));
        }
    }
}