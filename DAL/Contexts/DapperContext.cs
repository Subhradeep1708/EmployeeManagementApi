using MySqlConnector;
using System.Data;

namespace EmployeeManagement.DAL.Contexts
{
    public class DapperContext
    {
        private readonly IConfiguration _config;

        public DapperContext(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(_config.GetConnectionString("EmployeeDBConnection"));
        }
    }
}
