
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Interfaces
{
    public interface IDashboardRepository
    {
         Task<DashboardSummaryCardDTO> GetDashboardSummaryAsync();
    }
}
