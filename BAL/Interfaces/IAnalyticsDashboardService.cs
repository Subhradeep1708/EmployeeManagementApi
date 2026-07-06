using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Interfaces
{
    public interface IAnalyticsDashboardService
    {
        Task<AnalyticsDashboardDTO> GetAnalyticsDashboardAsync();
    }
}
