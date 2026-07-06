using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        [HttpGet("summary")]
        public async Task<ApiResponse<DashboardSummaryCardDTO>> DashboardSummaryCard()
        {
            ApiResponse<DashboardSummaryCardDTO> response = new()
            {
                Message = "",
                StatusCode = 200
            }; 
            try
            {
                var result = await _service.GetDashboardSummaryAsync();
                response.Success = true;
                response.Data = result;
                response.Message = "Dashboard Summary card data fetched";
                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = null;
                response.Message = "Failed to fetch dashboard summary.";
                response.StatusCode = 500;
                return response;
            }
            
        }
    }
}
