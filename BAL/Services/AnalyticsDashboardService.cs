using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Services
{
    public class AnalyticsService : IAnalyticsDashboardService
    {
        private readonly IAnalyticsDashboardRepository _repository;

        public AnalyticsService(IAnalyticsDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<AnalyticsDashboardDTO> GetAnalyticsDashboardAsync()
        {
            return await _repository.GetAnalyticsDashboardAsync();
        }
    }
}