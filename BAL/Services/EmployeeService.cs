using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResponse<EmployeeListDTO>> GetEmployeesAsync(EmployeeListRequestDTO request)
        {
            return await _repository.GetEmployeesAsync(request);
        }

        public async Task<bool> CreateEmployeeAsync(CreateEmployeeDTO dto)
        {
            return await _repository.CreateEmployeeAsync(dto);
        }

        public async Task<EmployeeDetailsDTO?> GetEmployeeByIdAsync(int employeeId)
        {
            return await _repository.GetEmployeeByIdAsync(employeeId);
        }

        public async Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDTO dto)
        {
            return await _repository.UpdateEmployeeAsync(employeeId, dto);
        }
    }
}
