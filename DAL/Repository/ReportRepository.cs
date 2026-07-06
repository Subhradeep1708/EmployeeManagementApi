using Dapper;
using EmployeeManagement.DAL.Contexts;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly DapperContext _context;

        public ReportRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<EmployeeListDTO>> GetEmployeeReportAsync()
        {
            using var connection = _context.CreateConnection();

            var sql = @"

                SELECT

                e.EmployeeId,
                e.EmployeeCode,
                CONCAT(e.FirstName,' ',e.LastName) FullName,
                e.Email,
                d.DepartmentName Department,
                des.DesignationName Designation,
                IFNULL(s.NetSalary,0) Salary,
                st.StatusName Status

                FROM Employees e

                INNER JOIN Departments d
                ON e.DepartmentId=d.DepartmentId

                INNER JOIN Designations des
                ON e.DesignationId=des.DesignationId

                INNER JOIN EmploymentStatuses st
                ON e.StatusId=st.StatusId

                LEFT JOIN SalaryHistory s
                ON e.EmployeeId=s.EmployeeId
                AND s.EffectiveTo IS NULL

                ORDER BY e.EmployeeId;

                ";

            return (await connection.QueryAsync<EmployeeListDTO>(sql)).ToList();
        }


        public async Task<List<AttendanceListDTO>> GetAttendanceReportAsync()
        {
            using var connection = _context.CreateConnection();

            var sql = @"
        SELECT
            a.AttendanceId,
            e.EmployeeId,
            CONCAT(e.FirstName,' ',e.LastName) AS EmployeeName,
            d.DepartmentName AS Department,
            a.AttendanceDate,
            a.Status,
            a.CheckIn,
            a.CheckOut
        FROM Attendance a
        INNER JOIN Employees e
            ON a.EmployeeId = e.EmployeeId
        INNER JOIN Departments d
            ON e.DepartmentId = d.DepartmentId
        ORDER BY a.AttendanceDate DESC, EmployeeName;
    ";

            return (await connection.QueryAsync<AttendanceListDTO>(sql)).ToList();
        }
    }
}