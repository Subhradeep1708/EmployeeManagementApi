using Dapper;
using EmployeeManagement.DAL.Contexts;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Repository
{
    public class AnalyticsRepository : IAnalyticsDashboardRepository
    {
        private readonly DapperContext _context;

        public AnalyticsRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<AnalyticsDashboardDTO> GetAnalyticsDashboardAsync()
        {
            using var connection = _context.CreateConnection();

            var sql = @"

SELECT
YEAR(HireDate) Year,
COUNT(*) EmployeesHired
FROM Employees
GROUP BY YEAR(HireDate)
ORDER BY Year;

SELECT
d.DepartmentName Department,
COUNT(e.EmployeeId) EmployeeCount
FROM Departments d
LEFT JOIN Employees e
ON d.DepartmentId=e.DepartmentId
GROUP BY d.DepartmentId,d.DepartmentName
ORDER BY EmployeeCount DESC;

SELECT
Status,
COUNT(*) Count
FROM Attendance
GROUP BY Status;

";

            using var multi = await connection.QueryMultipleAsync(sql);

            var hiringTrend =
                (await multi.ReadAsync<HiringTrendDTO>()).ToList();

            var departmentGrowth =
                (await multi.ReadAsync<DepartmentGrowthDTO>()).ToList();

            var attendancePattern =
                (await multi.ReadAsync<AttendancePatternDTO>()).ToList();

            return new AnalyticsDashboardDTO
            {
                HiringTrend = hiringTrend,
                DepartmentGrowth = departmentGrowth,
                AttendancePattern = attendancePattern
            };
        }
    }
}