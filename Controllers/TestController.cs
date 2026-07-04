using Dapper;
using EmployeeManagement.DAL.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly DapperContext _context;

        public TestController(DapperContext context)
        {
            _context = context;
        }

        [HttpGet("db")]
        public async Task<IActionResult> TestConnection()
        {
            using var connection = _context.CreateConnection();

            var result = await connection.QueryFirstAsync<int>("SELECT 1;");

            return Ok(result);
        }
    }
}
