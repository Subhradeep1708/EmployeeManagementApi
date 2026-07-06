using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Interfaces
{
    public interface IAttendanceRepository
    {
        Task<AttendanceResponseDTO> GetAttendanceAsync(AttendanceListRequestDTO request);
        Task<bool> UpdateAttendanceAsync(int attendanceId, UpdateAttendanceDTO dto);
        Task<bool> CreateAttendanceAsync(CreateAttendanceDTO dto);
    }
}
