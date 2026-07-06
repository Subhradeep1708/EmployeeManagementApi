using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;

        public AttendanceController(IAttendanceService service)
        {
            _service = service;
        }

        [HttpGet("get-attendance")]
        public async Task<ApiResponse<AttendanceResponseDTO>> GetAttendance([FromQuery] AttendanceListRequestDTO request)
        {
            ApiResponse<AttendanceResponseDTO> response = new()
            {
                StatusCode = 200
            };

            try
            {
                response.Data = await _service.GetAttendanceAsync(request);

                response.Success = true;

                response.Message = "Attendance fetched successfully.";

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

        [HttpPut("update-attendence/{id}")]
        public async Task<ApiResponse<bool>> UpdateAttendance(
        int id,
        UpdateAttendanceDTO dto)
        {
            ApiResponse<bool> response = new()
            {
                StatusCode = 200
            };

            try
            {
                response.Data = await _service.UpdateAttendanceAsync(id, dto);
                response.Success = true;
                response.Message = "Attendance updated successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.StatusCode = 500;
                response.Message = ex.Message;

                return response;
            }
        }

        [HttpPost("create-attendance")]
        public async Task<ApiResponse<bool>> CreateAttendance(CreateAttendanceDTO dto)
        {
            ApiResponse<bool> response = new()
            {
                StatusCode = 200
            };

            try
            {
                response.Data = await _service.CreateAttendanceAsync(dto);
                response.Success = true;
                response.Message = "Attendance marked successfully.";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = false;
                response.StatusCode = 500;
                response.Message = ex.Message;

                return response;
            }
        }
    }
}
