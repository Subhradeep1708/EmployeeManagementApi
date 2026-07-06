using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.BAL.Services;
using EmployeeManagement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsDashboardService _service;

        public AnalyticsController(IAnalyticsDashboardService service)
        {
            _service = service;
        }

        [HttpGet("dashboard")]
        public async Task<ApiResponse<AnalyticsDashboardDTO>> GetAnalyticsDashboard()
        {
            ApiResponse<AnalyticsDashboardDTO> response = new()
            {
                StatusCode = 200
            };

            try
            {
                response.Data = await _service.GetAnalyticsDashboardAsync();
                response.Success = true;
                response.Message = "Analytics data fetched successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.StatusCode = 500;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}