using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedResponse<EmployeeListDTO>> GetEmployeesAsync(EmployeeListRequestDTO request);
        Task<bool> CreateEmployeeAsync(CreateEmployeeDTO dto);
        Task<EmployeeDetailsDTO?> GetEmployeeByIdAsync(int employeeId);
        Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDTO dto);
        Task<bool> DeleteEmployeeAsync(int employeeId);
    }
}
