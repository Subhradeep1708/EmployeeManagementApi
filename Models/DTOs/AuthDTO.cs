namespace EmployeeManagement.Models.DTOs
{
    
        public class LoginRequestDTO
        {
            public string Username { get; set; } = "";

            public string Password { get; set; } = "";
        }
    
    public class LoginResponseDTO
    {
        public string Token { get; set; } = "";

        public string Username { get; set; } = "";

        public string Role { get; set; } = "";

        public int UserId { get; set; }
    }
}
