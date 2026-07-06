using Dapper;
using EmployeeManagement.DAL.Contexts;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Repository
{
    public class DashboardRepository :IDashboardRepository
    {
        private readonly DapperContext _context;

        public DashboardRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<DashboardSummaryCardDTO> GetDashboardSummaryAsync()
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                    SELECT
                    (SELECT COUNT(*) FROM Employees) AS TotalEmployees,

                    (SELECT COUNT(*)
                     FROM Employees
                     WHERE StatusId = (
                        SELECT StatusId
                        FROM EmploymentStatuses
                        WHERE StatusName = 'Active'
                     )) AS ActivePersonnel,

                    (SELECT COUNT(*)
                     FROM Attendance
                     WHERE AttendanceDate = CURDATE()
                     AND Status = 'Present') AS Attendance,

                    (SELECT IFNULL(AVG(NetSalary),0)
                     FROM SalaryHistory
                     WHERE EffectiveTo IS NULL) AS AverageSalary,

                    (SELECT IFNULL(SUM(NetSalary),0)
                     FROM SalaryHistory
                     WHERE EffectiveTo IS NULL) AS AnnualPayroll;

                    SELECT
                        d.DepartmentName,
                        COUNT(e.EmployeeId) EmployeeCount
                    FROM Departments d
                    LEFT JOIN Employees e
                    ON d.DepartmentId = e.DepartmentId
                    GROUP BY d.DepartmentId,d.DepartmentName
                    ORDER BY EmployeeCount DESC;
                ";

            using var multi = await connection.QueryMultipleAsync(sql);

            var summary = await multi.ReadFirstAsync<DashboardSummaryCardDTO>();

            summary.DepartmentBreakdown = (
                await multi.ReadAsync<DepartmentBreakdownDto>()
            ).ToList();

            return summary;
        }
    }
}
