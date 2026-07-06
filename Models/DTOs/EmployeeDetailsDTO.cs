namespace EmployeeManagement.Models.DTOs
{
    public class EmployeeDetailsDTO
    {
        public int EmployeeId { get; set; }

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

        public string DepartmentName { get; set; } = string.Empty;

        public int DesignationId { get; set; }

        public string DesignationName { get; set; } = string.Empty;

        public int StatusId { get; set; }

        public string StatusName { get; set; } = string.Empty;

        public decimal BasicSalary { get; set; }

        public decimal Bonus { get; set; }

        public decimal Deduction { get; set; }

        public decimal NetSalary { get; set; }
    }
}
