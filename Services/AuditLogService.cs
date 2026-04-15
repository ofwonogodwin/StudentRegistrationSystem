using Microsoft.EntityFrameworkCore;
using StudentRegistrationSystem.Data;
using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Services.Interfaces;

namespace StudentRegistrationSystem.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ApplicationDbContext _context;

        public AuditLogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task RecordAsync(string action, string entityName, string? entityId, string? details, string performedBy, string performedByRole, string? ipAddress)
        {
            _context.AuditLogs.Add(new AuditLog
            {
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                Details = details,
                PerformedBy = performedBy,
                PerformedByRole = performedByRole,
                IpAddress = ipAddress,
                CreatedAt = DateTime.Now
            });

            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<AuditLog>> GetRecentLogsAsync(int take = 50)
        {
            return await _context.AuditLogs
                .AsNoTracking()
                .OrderByDescending(log => log.CreatedAt)
                .Take(take)
                .ToListAsync();
        }
    }
}