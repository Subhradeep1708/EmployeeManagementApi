using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.Models.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.BAL.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;

    private readonly IAuthRepository _repository;

    public AuthService(IConfiguration config,
        IAuthRepository repository)
    {
        _config = config;
        _repository = repository;
    }

    public async Task<LoginResponseDTO?> LoginAsync(LoginRequestDTO request)
    {
        var user = await _repository.LoginAsync(request);

        if (user == null)
            return null;

        var claims = new[]
        {
            new Claim("UserId", user.UserId.ToString()),
            new Claim(ClaimTypes.Name,user.Username),
            new Claim(ClaimTypes.Role,user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!)
        );

        var credentials =
            new SigningCredentials(key,
                SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credentials
        );

        user.Token =
            new JwtSecurityTokenHandler().WriteToken(token);

        return user;
    }
}