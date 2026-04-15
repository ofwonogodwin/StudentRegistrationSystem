using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Models.ViewModels;

namespace StudentRegistrationSystem.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IReadOnlyList<string>> GetAvailableCoursesAsync();
        Task<PagedResult<Student>> GetStudentsAsync(string? searchTerm, string? courseFilter, int? yearFilter, string? sortOrder, int pageIndex, int pageSize);
        Task<byte[]> ExportStudentsCsvAsync(string? searchTerm, string? courseFilter, int? yearFilter, string? sortOrder);
    }
}