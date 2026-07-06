namespace EmployeeManagement.Models.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public String? Message { get; set; }
        public required int StatusCode { get; set; }

    }

    public class PagedResponse<T>
    {
        public List<T> Items { get; set; } = [];

        public int TotalRecords { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }
    }
}
