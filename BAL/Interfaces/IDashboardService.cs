using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryCardDTO> GetDashboardSummaryAsync();
    }
}
