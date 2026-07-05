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
                ";

            return await connection.QueryFirstAsync<DashboardSummaryCardDTO>(sql);
        }
    }
}
