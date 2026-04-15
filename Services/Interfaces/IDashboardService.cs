using StudentRegistrationSystem.Models.ViewModels;

namespace StudentRegistrationSystem.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryViewModel> GetSummaryAsync();
    }
}