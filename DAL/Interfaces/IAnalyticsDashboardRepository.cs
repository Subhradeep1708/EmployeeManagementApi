using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Interfaces
{
    public interface IAnalyticsDashboardRepository
    {
        Task<AnalyticsDashboardDTO> GetAnalyticsDashboardAsync();
    }
}
