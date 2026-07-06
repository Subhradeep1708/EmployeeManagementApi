using Dapper;
using EmployeeManagement.DAL.Contexts;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly DapperContext _context;

        public AttendanceRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<AttendanceResponseDTO> GetAttendanceAsync(AttendanceListRequestDTO request)
        {
            using var connection = _context.CreateConnection();

            var offset = (request.Page - 1) * request.PageSize;

            var sql = @"

SELECT
    a.AttendanceId,
    e.EmployeeId,
    CONCAT(e.FirstName,' ',e.LastName) EmployeeName,
    d.DepartmentName Department,
    a.AttendanceDate,
    a.Status,
    a.CheckIn,
    a.CheckOut

FROM Attendance a

INNER JOIN Employees e
ON a.EmployeeId = e.EmployeeId

INNER JOIN Departments d
ON e.DepartmentId = d.DepartmentId

WHERE

(@Search IS NULL
OR CONCAT(e.FirstName,' ',e.LastName) LIKE CONCAT('%',@Search,'%')
OR e.Email LIKE CONCAT('%',@Search,'%'))

AND (@Date IS NULL OR a.AttendanceDate=@Date)

AND (@Status IS NULL OR a.Status=@Status)

ORDER BY a.AttendanceDate DESC

LIMIT @PageSize
OFFSET @Offset;



SELECT COUNT(*)

FROM Attendance a

INNER JOIN Employees e
ON a.EmployeeId=e.EmployeeId

WHERE

(@Search IS NULL
OR CONCAT(e.FirstName,' ',e.LastName) LIKE CONCAT('%',@Search,'%')
OR e.Email LIKE CONCAT('%',@Search,'%'))

AND (@Date IS NULL OR a.AttendanceDate=@Date)

AND (@Status IS NULL OR a.Status=@Status);



SELECT

SUM(CASE WHEN Status='Present' THEN 1 ELSE 0 END) Present,

SUM(CASE WHEN Status='Absent' THEN 1 ELSE 0 END) Absent,

SUM(CASE WHEN Status='Leave' THEN 1 ELSE 0 END) `Leave`,

SUM(CASE WHEN Status='Half Day' THEN 1 ELSE 0 END) HalfDay,

SUM(CASE WHEN Status='WFH' THEN 1 ELSE 0 END) WFH,

ROUND(
SUM(CASE WHEN Status='Present' THEN 1 ELSE 0 END)
/
COUNT(*) * 100,
2
) AttendanceRate

FROM Attendance

WHERE (@Date IS NULL OR AttendanceDate=@Date);

";

            using var multi = await connection.QueryMultipleAsync(sql, new
            {
                request.Search,
                request.Date,
                request.Status,
                request.PageSize,
                Offset = offset
            });

            var attendance = (await multi.ReadAsync<AttendanceListDTO>()).ToList();

            var totalRecords = await multi.ReadFirstAsync<int>();

            var summary = await multi.ReadFirstAsync<AttendanceSummaryDTO>();

            return new AttendanceResponseDTO
            {
                Items = attendance,
                Summary = summary,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
            };
        }

        public async Task<bool> UpdateAttendanceAsync(int attendanceId, UpdateAttendanceDTO dto)
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                    UPDATE Attendance
                    SET

                    Status=@Status,
                    CheckIn=@CheckIn,
                    CheckOut=@CheckOut,
                    Remarks=@Remarks

                    WHERE AttendanceId=@AttendanceId;
                    ";

            var rows = await connection.ExecuteAsync(sql, new
            {
                AttendanceId = attendanceId,
                dto.Status,
                dto.CheckIn,
                dto.CheckOut,
                dto.Remarks
            });

            return rows > 0;
        }


        public async Task<bool> CreateAttendanceAsync(CreateAttendanceDTO dto)
        {
            using var connection = _context.CreateConnection();

            var sql = @"

                    INSERT INTO Attendance
                    (
                    EmployeeId,
                    AttendanceDate,
                    CheckIn,
                    CheckOut,
                    Status,
                    Remarks
                    )

                    VALUES
                    (
                    @EmployeeId,
                    @AttendanceDate,
                    @CheckIn,
                    @CheckOut,
                    @Status,
                    @Remarks
                    );
                    ";

            var rows = await connection.ExecuteAsync(sql, dto);

            return rows > 0;
        }
    }
}