using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Services
{
    public class DashboardService: IDashboardService
    {
     
        private readonly IDashboardRepository _repository;
        public DashboardService(IDashboardRepository repository)
        {
            _repository = repository;
        }
        public async Task<DashboardSummaryCardDTO> GetDashboardSummaryAsync()
        {
            return await _repository.GetDashboardSummaryAsync();
        }
    
    }
}
