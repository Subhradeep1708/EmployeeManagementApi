namespace EmployeeManagement.Models.DTOs
{
    public class EmployeeListDTO
    {
        public int EmployeeId { get; set; }
        public string EmployeeCode { get; set; } = "";
        public string FullName { get; set; } = "";
        public string Email { get; set; } = "";
        public string Department { get; set; } = "";
        public string Designation { get; set; } = "";
        public decimal Salary { get; set; }
        public string Status { get; set; } = "";
    }

    public class EmployeeListRequestDTO
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public int? DepartmentId { get; set; }

        public int? StatusId { get; set; }
    }
}
