namespace EmployeeManagement.Models.DTOs
{
    public class HiringTrendDTO
    {
        public int Year { get; set; }
        public int EmployeesHired { get; set; }
    }
    public class DepartmentGrowthDTO
    {
        public string Department { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
    }
    public class AttendancePatternDTO
    {
        public string Status { get; set; } = string.Empty;
        public int Count { get; set; }
    }
    public class AnalyticsDashboardDTO
    {
        public List<HiringTrendDTO> HiringTrend { get; set; } = [];

        public List<DepartmentGrowthDTO> DepartmentGrowth { get; set; } = [];

        public List<AttendancePatternDTO> AttendancePattern { get; set; } = [];
    }
}
