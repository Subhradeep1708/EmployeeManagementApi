namespace EmployeeManagement.Models.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public required String Message { get; set; }
        public required int StatusCode { get; set; }

    }
}
