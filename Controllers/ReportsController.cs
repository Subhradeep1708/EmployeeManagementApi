using EmployeeManagement.BAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
        }

        [HttpGet("employees/pdf")]
        public async Task<IActionResult> ExportEmployeesPdf()
        {
            var pdf = await _service.ExportEmployeesPdfAsync();

            return File(
                pdf,
                "application/pdf",
                "Employees.pdf"
            );
        }

        [HttpGet("employees/excel")]
        public async Task<IActionResult> ExportEmployeesExcel()
        {
            var file = await _service.ExportEmployeesExcelAsync();

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Employees.xlsx");
        }

        [HttpGet("attendance/pdf")]
        public async Task<IActionResult> ExportAttendancePdf()
        {
            var pdf = await _service.ExportAttendancePdfAsync();

            return File(
                pdf,
                "application/pdf",
                "AttendanceReport.pdf");
        }
    }
}