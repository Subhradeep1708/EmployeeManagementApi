namespace EmployeeManagement.BAL.Interfaces
{
    public interface IReportService
    {
        Task<byte[]> ExportEmployeesPdfAsync();
        Task<byte[]> ExportEmployeesExcelAsync();
        Task<byte[]> ExportAttendancePdfAsync();
    }
}
