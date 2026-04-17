using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models.ViewModels;
using StudentRegistrationSystem.Services.Interfaces;

namespace StudentRegistrationSystem.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryViewModel> GetSummaryAsync()
        {
            var studentsByCourse = await _context.Students
                .AsNoTracking()
                .GroupBy(student => student.Course)
                .Select(group => new ChartDataPoint
                {
                    Label = group.Key,
                    Value = group.Count()
                })
                .OrderByDescending(point => point.Value)
                .ThenBy(point => point.Label)
                .ToListAsync();

            var studentsByYear = await _context.Students
                .AsNoTracking()
                .GroupBy(student => student.YearOfStudy)
                .Select(group => new ChartDataPoint
                {
                    Label = $"Year {group.Key}",
                    Value = group.Count()
                })
                .OrderBy(point => point.Label)
                .ToListAsync();

            return new DashboardSummaryViewModel
            {
                TotalStudents = await _context.Students.CountAsync(),
                TotalUsers = await _context.Users.CountAsync(),
                TotalCourses = await _context.Students.Select(student => student.Course).Distinct().CountAsync(),
                StudentsByCourse = studentsByCourse,
                StudentsByYear = studentsByYear
            };
        }
    }
}