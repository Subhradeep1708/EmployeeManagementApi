namespace EmployeeManagement.Models.DTOs
{
    public class AttendanceListRequestDTO
    {
        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public DateTime? Date { get; set; }

        public string? Status { get; set; }
    }

    public class AttendanceListDTO
    {
        public int AttendanceId { get; set; }

        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public DateTime AttendanceDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public TimeSpan? CheckIn { get; set; }

        public TimeSpan? CheckOut { get; set; }
    }

    public class AttendanceSummaryDTO
    {
        public int Present { get; set; }

        public int Absent { get; set; }

        public int Leave { get; set; }

        public int HalfDay { get; set; }

        public int WFH { get; set; }

        public decimal AttendanceRate { get; set; }
    }

    public class AttendanceResponseDTO
    {
        public List<AttendanceListDTO> Items { get; set; } = [];

        public AttendanceSummaryDTO Summary { get; set; } = new();

        public int TotalRecords { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }

    public class UpdateAttendanceDTO
    {
        public string Status { get; set; } = string.Empty;

        public TimeSpan? CheckIn { get; set; }

        public TimeSpan? CheckOut { get; set; }

        public string? Remarks { get; set; }
    }

    public class CreateAttendanceDTO
    {
        public int EmployeeId { get; set; }

        public DateTime AttendanceDate { get; set; }

        public string Status { get; set; } = string.Empty;

        public TimeSpan? CheckIn { get; set; }

        public TimeSpan? CheckOut { get; set; }

        public string? Remarks { get; set; }
    }
}
