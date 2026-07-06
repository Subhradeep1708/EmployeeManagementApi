using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Interfaces
{
    public interface IReportRepository
    {
        Task<List<EmployeeListDTO>> GetEmployeeReportAsync();
        Task<List<AttendanceListDTO>> GetAttendanceReportAsync();
    }
}
