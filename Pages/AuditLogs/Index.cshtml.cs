using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentRegistrationSystem.Models;
using StudentRegistrationSystem.Services.Interfaces;

namespace StudentRegistrationSystem.Pages.AuditLogs
{
    [Authorize(Policy = "AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly IAuditLogService _auditLogService;

        public IndexModel(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        public IReadOnlyList<AuditLog> Logs { get; set; } = Array.Empty<AuditLog>();

        public async Task OnGetAsync()
        {
            Logs = await _auditLogService.GetRecentLogsAsync(100);
        }
    }
}