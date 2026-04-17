using StudentRegistrationSystem.Models;

namespace StudentRegistrationSystem.Services.Interfaces
{
    public interface IAuditLogService
    {
        Task RecordAsync(string action, string entityName, string? entityId, string? details, string performedBy, string performedByRole, string? ipAddress);
        Task<IReadOnlyList<AuditLog>> GetRecentLogsAsync(int take = 50);
    }
}