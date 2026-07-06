namespace EmployeeManagement.Models.DTOs
{
    public class DashboardSummaryCardDTO
    {
        public required int TotalEmployees { get; set; }
        public required int ActivePersonnel { get; set; }
        public required int Attendence { get; set; }
        public required decimal AverageSalary { get; set; }
        public required decimal AnnualPayroll { get; set; }
        public List<DepartmentBreakdownDto> DepartmentBreakdown { get; set; } = [];

    }

    public class DepartmentBreakdownDto
    {
        public string DepartmentName { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
    }
}
