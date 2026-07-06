using Dapper;
using EmployeeManagement.DAL.Contexts;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;

namespace EmployeeManagement.DAL.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly DapperContext _context;

    public AuthRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
    {
        using var connection = _context.CreateConnection();

        var sql = @"

SELECT

u.UserId,
u.Username,
u.PasswordHash,
r.RoleName Role

FROM Users u

INNER JOIN Roles r
ON u.RoleId=r.RoleId

WHERE
u.Username=@Username
AND u.IsActive=1;

";

        var user = await connection.QueryFirstOrDefaultAsync<dynamic>(
            sql,
            new
            {
                request.Username
            });

        if (user == null)
            return null;

        // Plain text comparison (for assignment)
        if ((string)user.PasswordHash != request.Password)
            return null;

        return new LoginResponseDTO
        {
            UserId = user.UserId,
            Username = user.Username,
            Role = user.Role
        };
    }
}