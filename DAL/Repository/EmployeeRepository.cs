using EmployeeManagement.DAL.Contexts;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;
using Dapper;

namespace EmployeeManagement.DAL.Repository
{
    public class EmployeeRepository: IEmployeeRepository
    {
        private readonly DapperContext _context;

        public EmployeeRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<EmployeeListDTO>> GetEmployeesAsync(EmployeeListRequestDTO request)
        {
            using var connection = _context.CreateConnection();

            var offset = (request.Page - 1) * request.PageSize;

            var sql = @"
                       SELECT
                            e.EmployeeId,
                            e.EmployeeCode,
                            CONCAT(e.FirstName,' ',e.LastName) AS FullName,
                            e.Email,
                            d.DepartmentName AS Department,
                            des.DesignationName AS Designation,
                            IFNULL(s.NetSalary,0) AS Salary,
                            st.StatusName AS Status
                        FROM Employees e
                        INNER JOIN Departments d
                            ON e.DepartmentId = d.DepartmentId
                        INNER JOIN Designations des
                            ON e.DesignationId = des.DesignationId
                        INNER JOIN EmploymentStatuses st
                            ON e.StatusId = st.StatusId
                        LEFT JOIN SalaryHistory s
                            ON e.EmployeeId = s.EmployeeId
                            AND s.EffectiveTo IS NULL

                        WHERE
                        (@Search IS NULL
                        OR CONCAT(e.FirstName,' ',e.LastName) LIKE CONCAT('%',@Search,'%')
                        OR e.Email LIKE CONCAT('%',@Search,'%'))

                        AND (@DepartmentId IS NULL OR e.DepartmentId = @DepartmentId)

                        AND (@StatusId IS NULL OR e.StatusId = @StatusId)

                        ORDER BY e.EmployeeId

                        LIMIT @PageSize
                        OFFSET @Offset;

                        SELECT COUNT(*)
                        FROM Employees e
                        WHERE
                        (@Search IS NULL
                        OR CONCAT(e.FirstName,' ',e.LastName) LIKE CONCAT('%',@Search,'%')
                        OR e.Email LIKE CONCAT('%',@Search,'%'))

                        AND (@DepartmentId IS NULL OR e.DepartmentId = @DepartmentId)

                        AND (@StatusId IS NULL OR e.StatusId = @StatusId);
                        ";

            using var multi = await connection.QueryMultipleAsync(sql, new
            {
                request.Search,
                request.DepartmentId,
                request.StatusId,
                request.PageSize,
                Offset = offset
            });

            var employees = (await multi.ReadAsync<EmployeeListDTO>()).ToList();

            var totalRecords = await multi.ReadFirstAsync<int>();

            return new PagedResponse<EmployeeListDTO>
            {
                Items = employees,
                TotalRecords = totalRecords,
                Page = request.Page,
                PageSize = request.PageSize,
                TotalPages = (int)Math.Ceiling((double)totalRecords / request.PageSize)
            };
        }


        public async Task<bool> CreateEmployeeAsync(CreateEmployeeDTO dto)
        {
            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                var employeeSql = @"
                    INSERT INTO Employees
                    (
                    EmployeeCode,
                    FirstName,
                    LastName,
                    Gender,
                    DateOfBirth,
                    Email,
                    Phone,
                    Address,
                    HireDate,
                    DepartmentId,
                    DesignationId,
                    StatusId
                    )

                    VALUES
                    (
                    @EmployeeCode,
                    @FirstName,
                    @LastName,
                    @Gender,
                    @DateOfBirth,
                    @Email,
                    @Phone,
                    @Address,
                    @HireDate,
                    @DepartmentId,
                    @DesignationId,
                    @StatusId
                    );

                    SELECT LAST_INSERT_ID();
                    ";

                int employeeId = await connection.ExecuteScalarAsync<int>(
                    employeeSql,
                    dto,
                    transaction);

                var salarySql = @"
                            INSERT INTO SalaryHistory
                            (
                            EmployeeId,
                            BasicSalary,
                            Bonus,
                            Deduction,
                            EffectiveFrom
                            )
                            VALUES
                            (
                            @EmployeeId,
                            @BasicSalary,
                            @Bonus,
                            @Deduction,
                            CURDATE()
                            );
                            ";

                await connection.ExecuteAsync(
                    salarySql,
                    new
                    {
                        EmployeeId = employeeId,
                        dto.BasicSalary,
                        dto.Bonus,
                        dto.Deduction
                    },
                    transaction);

                transaction.Commit();

                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }


        public async Task<EmployeeDetailsDTO?> GetEmployeeByIdAsync(int employeeId)
        {
            using var connection = _context.CreateConnection();

            var sql = @"
                    SELECT
                        e.EmployeeId,
                        e.EmployeeCode,
                        e.FirstName,
                        e.LastName,
                        e.Gender,
                        e.DateOfBirth,
                        e.Email,
                        e.Phone,
                        e.Address,
                        e.HireDate,

                        d.DepartmentId,
                        d.DepartmentName,

                        des.DesignationId,
                        des.DesignationName,

                        st.StatusId,
                        st.StatusName,

                        s.BasicSalary,
                        s.Bonus,
                        s.Deduction,
                        s.NetSalary

                    FROM Employees e

                    INNER JOIN Departments d
                        ON e.DepartmentId = d.DepartmentId

                    INNER JOIN Designations des
                        ON e.DesignationId = des.DesignationId

                    INNER JOIN EmploymentStatuses st
                        ON e.StatusId = st.StatusId

                    LEFT JOIN SalaryHistory s
                        ON e.EmployeeId = s.EmployeeId
                        AND s.EffectiveTo IS NULL

                    WHERE e.EmployeeId = @EmployeeId;
                ";

            return await connection.QueryFirstOrDefaultAsync<EmployeeDetailsDTO>(
                sql,
                new
                {
                    EmployeeId = employeeId
                });
        }


        public async Task<bool> UpdateEmployeeAsync(int employeeId, UpdateEmployeeDTO dto)
        {
            using var connection = _context.CreateConnection();

            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                var employeeSql = @"
UPDATE Employees
SET

EmployeeCode=@EmployeeCode,
FirstName=@FirstName,
LastName=@LastName,
Gender=@Gender,
DateOfBirth=@DateOfBirth,
Email=@Email,
Phone=@Phone,
Address=@Address,
HireDate=@HireDate,
DepartmentId=@DepartmentId,
DesignationId=@DesignationId,
StatusId=@StatusId

WHERE EmployeeId=@EmployeeId;
";

                await connection.ExecuteAsync(
                    employeeSql,
                    new
                    {
                        EmployeeId = employeeId,

                        dto.EmployeeCode,
                        dto.FirstName,
                        dto.LastName,
                        dto.Gender,
                        dto.DateOfBirth,
                        dto.Email,
                        dto.Phone,
                        dto.Address,
                        dto.HireDate,
                        dto.DepartmentId,
                        dto.DesignationId,
                        dto.StatusId
                    },
                    transaction
                );

                var salarySql = @"
UPDATE SalaryHistory
SET

BasicSalary=@BasicSalary,
Bonus=@Bonus,
Deduction=@Deduction

WHERE EmployeeId=@EmployeeId
AND EffectiveTo IS NULL;
";

                await connection.ExecuteAsync(
                    salarySql,
                    new
                    {
                        EmployeeId = employeeId,

                        dto.BasicSalary,
                        dto.Bonus,
                        dto.Deduction
                    },
                    transaction
                );

                transaction.Commit();

                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
