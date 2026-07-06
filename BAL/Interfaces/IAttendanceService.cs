using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.BAL.Interfaces
{
    public interface IAttendanceService
    {
        Task<AttendanceResponseDTO> GetAttendanceAsync(AttendanceListRequestDTO request);
        Task<bool> UpdateAttendanceAsync(int attendanceId, UpdateAttendanceDTO dto);
        Task<bool> CreateAttendanceAsync(CreateAttendanceDTO dto);

    }
}
