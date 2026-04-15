using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Models.ViewModels;
using StudentRegistrationSystem.Services.Interfaces;

namespace StudentRegistrationSystem.Services
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<string>> GetAvailableCoursesAsync()
        {
            return await _context.Students
                .AsNoTracking()
                .Select(student => student.Course)
                .Distinct()
                .OrderBy(course => course)
                .ToListAsync();
        }

        public async Task<PagedResult<Student>> GetStudentsAsync(string? searchTerm, string? courseFilter, int? yearFilter, string? sortOrder, int pageIndex, int pageSize)
        {
            var query = BuildFilteredQuery(searchTerm, courseFilter, yearFilter, sortOrder);
            return await PagedResult<Student>.CreateAsync(query, pageIndex, pageSize);
        }

        public async Task<byte[]> ExportStudentsCsvAsync(string? searchTerm, string? courseFilter, int? yearFilter, string? sortOrder)
        {
            var students = await BuildFilteredQuery(searchTerm, courseFilter, yearFilter, sortOrder).ToListAsync();
            var csv = new StringBuilder();

            csv.AppendLine("FullName,RegistrationNumber,Email,PhoneNumber,Course,YearOfStudy,DateOfBirth,Gender,CreatedAt,UpdatedAt");

            foreach (var student in students)
            {
                csv.AppendLine(string.Join(",",
                    EscapeCsv(student.FullName),
                    EscapeCsv(student.RegistrationNumber),
                    EscapeCsv(student.Email),
                    EscapeCsv(student.PhoneNumber),
                    EscapeCsv(student.Course),
                    student.YearOfStudy.ToString(CultureInfo.InvariantCulture),
                    student.DateOfBirth?.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) ?? string.Empty,
                    EscapeCsv(student.Gender),
                    student.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture),
                    student.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture) ?? string.Empty));
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private IQueryable<Student> BuildFilteredQuery(string? searchTerm, string? courseFilter, int? yearFilter, string? sortOrder)
        {
            var query = _context.Students.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var searchLower = searchTerm.ToLower();
                query = query.Where(student =>
                    student.FullName.ToLower().Contains(searchLower) ||
                    student.RegistrationNumber.ToLower().Contains(searchLower) ||
                    student.Email.ToLower().Contains(searchLower) ||
                    student.Course.ToLower().Contains(searchLower));
            }

            if (!string.IsNullOrWhiteSpace(courseFilter))
            {
                query = query.Where(student => student.Course == courseFilter);
            }

            if (yearFilter.HasValue)
            {
                query = query.Where(student => student.YearOfStudy == yearFilter.Value);
            }

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(student => student.FullName),
                "reg_asc" => query.OrderBy(student => student.RegistrationNumber),
                "reg_desc" => query.OrderByDescending(student => student.RegistrationNumber),
                "course_asc" => query.OrderBy(student => student.Course),
                "course_desc" => query.OrderByDescending(student => student.Course),
                "year_asc" => query.OrderBy(student => student.YearOfStudy),
                "year_desc" => query.OrderByDescending(student => student.YearOfStudy),
                "date_asc" => query.OrderBy(student => student.CreatedAt),
                "date_desc" => query.OrderByDescending(student => student.CreatedAt),
                _ => query.OrderBy(student => student.FullName)
            };

            return query;
        }

        private static string EscapeCsv(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var escaped = value.Replace("\"", "\"\"");
            return $"\"{escaped}\"";
        }
    }
}