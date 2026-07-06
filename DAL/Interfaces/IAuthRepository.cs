using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Interfaces
{
    public interface IAuthRepository
    {
        Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request);
    }
}
