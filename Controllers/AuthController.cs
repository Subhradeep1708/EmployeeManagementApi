using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpPost("login")]
    public async Task<ApiResponse<LoginResponseDTO>> Login(
        LoginRequestDTO request)
    {
        ApiResponse<LoginResponseDTO> response = new() {
            StatusCode = 200
        };

        try
        {
            var result = await _service.LoginAsync(request);

            if (result == null)
            {
                response.Success = false;
                response.Message = "Invalid username or password.";
                response.StatusCode = 401;

                return response;
            }

            response.Success = true;
            response.Data = result;
            response.Message = "Login successful.";
            response.StatusCode = 200;

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