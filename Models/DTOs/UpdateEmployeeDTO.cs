namespace EmployeeManagement.Models.DTOs
{
    public class UpdateEmployeeDTO
    {
        public string EmployeeCode { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string? LastName { get; set; }

        public string Gender { get; set; } = string.Empty;

        public DateTime? DateOfBirth { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public DateTime HireDate { get; set; }

        public int DepartmentId { get; set; }

        public int DesignationId { get; set; }

        public int StatusId { get; set; }

        public decimal BasicSalary { get; set; }

        public decimal Bonus { get; set; }

        public decimal Deduction { get; set; }
    }
    
}
