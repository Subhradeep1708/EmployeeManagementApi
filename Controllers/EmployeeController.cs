using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    
    
        [ApiController]
        [Route("api/[controller]")]
        public class EmployeesController : ControllerBase
        {
            private readonly IEmployeeService _service;

            public EmployeesController(IEmployeeService service)
            {
                _service = service;
            }

            [HttpGet("get-employees")]
            public async Task<ApiResponse<PagedResponse<EmployeeListDTO>>> GetEmployees(
                [FromQuery] EmployeeListRequestDTO request)
            {
                ApiResponse<PagedResponse<EmployeeListDTO>> response = new()
                {
                    Message = "",
                    StatusCode = 200
                };

                try
                {
                    var result = await _service.GetEmployeesAsync(request);

                    response.Success = true;
                    response.Data = result;
                    response.Message = "Employees fetched successfully.";

                    return response;
                }
                catch
                {
                    response.Success = false;
                    response.Data = null;
                    response.StatusCode = 500;
                    response.Message = "Failed to fetch employees.";

                    return response;
                }
            }


        [HttpPost("create-employee")]
        public async Task<ApiResponse<bool>> CreateEmployee(CreateEmployeeDTO dto)
        {
            ApiResponse<bool> response = new()
            {
                Message = " ",
                StatusCode = 200
            };

            try
            {
                response.Data = await _service.CreateEmployeeAsync(dto);
                response.Success = true;
                response.Message = "Employee created successfully.";

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

        [HttpGet("get-employee-by-id/{id}")]
        public async Task<ApiResponse<EmployeeDetailsDTO>> GetEmployeeById(int id)
        {
            ApiResponse<EmployeeDetailsDTO> response = new()
            {
                Message = "",
                StatusCode = 200
            };

            try
            {
                var result = await _service.GetEmployeeByIdAsync(id);

                if (result == null)
                {
                    response.Success = false;
                    response.StatusCode = 404;
                    response.Message = "Employee not found.";

                    return response;
                }

                response.Success = true;
                response.Data = result;
                response.Message = "Employee fetched successfully.";

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

      
        
        [HttpPut("update-employee/{id}")]
        public async Task<ApiResponse<bool>> UpdateEmployee(int id, UpdateEmployeeDTO dto)
        {
            ApiResponse<bool> response = new()
            {
                StatusCode = 200
            };

            try
            {
                var result = await _service.UpdateEmployeeAsync(id, dto);

                response.Success = true;
                response.Data = result;
                response.Message = "Employee updated successfully.";

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

        [HttpDelete("delete-employee/{id}")]
        public async Task<ApiResponse<bool>> DeleteEmployee(int id)
        {
            ApiResponse<bool> response = new()
            {
                StatusCode = 200
            };

            try
            {
                response.Data = await _service.DeleteEmployeeAsync(id);
                response.Success = true;
                response.Message = "Employee deleted successfully.";

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
