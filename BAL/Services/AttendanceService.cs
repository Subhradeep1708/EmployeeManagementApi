using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Services
{
    
        public class AttendanceService : IAttendanceService
        {
            private readonly IAttendanceRepository _repository;

            public AttendanceService(IAttendanceRepository repository)
            {
                _repository = repository;
            }

            public async Task<AttendanceResponseDTO> GetAttendanceAsync(AttendanceListRequestDTO request)
            {
                return await _repository.GetAttendanceAsync(request);
            }

            public async Task<bool> UpdateAttendanceAsync(int attendanceId, UpdateAttendanceDTO dto)
            {
                return await _repository.UpdateAttendanceAsync(attendanceId, dto);
            }
        public async Task<bool> CreateAttendanceAsync(CreateAttendanceDTO dto)
        {
            return await _repository.CreateAttendanceAsync(dto);
        }
    }
    
}
