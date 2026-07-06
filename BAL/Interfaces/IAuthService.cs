using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
    }
}
