using EmployeeManagement.BAL.Interfaces;
using EmployeeManagement.DAL.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ClosedXML.Excel;

namespace EmployeeManagement.BAL.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;

        public ReportService(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<byte[]> ExportEmployeesPdfAsync()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var employees = await _repository.GetEmployeeReportAsync();

            static IContainer CellStyle(IContainer container)
            {
                return container
                    .BorderBottom(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .PaddingVertical(8)
                    .PaddingHorizontal(6)
                    .AlignMiddle();
            }

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(25);

                    page.Header().Column(column =>
                    {
                        column.Item()
                            .Text("Employee Management System")
                            .FontSize(22)
                            .Bold()
                            .FontColor(Colors.Blue.Darken2);

                        column.Item()
                            .Text("Employee Directory Report")
                            .FontSize(16)
                            .SemiBold();

                        column.Item()
                            .PaddingTop(5)
                            .LineHorizontal(1)
                            .LineColor(Colors.Grey.Lighten2);
                    });

                    page.Content().PaddingVertical(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);   // Name
                            columns.RelativeColumn(2);   // Department
                            columns.RelativeColumn(2);   // Designation
                            columns.RelativeColumn();    // Salary
                            columns.RelativeColumn();    // Status
                        });

                        table.Header(header =>
                        {
                            header.Cell()
                                .Element(CellStyle)
                                .Text("Name")
                                .Bold();

                            header.Cell()
                                .Element(CellStyle)
                                .Text("Department")
                                .Bold();

                            header.Cell()
                                .Element(CellStyle)
                                .Text("Designation")
                                .Bold();

                            header.Cell()
                                .Element(CellStyle)
                                .AlignRight()
                                .Text("Salary")
                                .Bold();

                            header.Cell()
                                .Element(CellStyle)
                                .Text("Status")
                                .Bold();
                        });

                        foreach (var emp in employees)
                        {
                            table.Cell()
                                .Element(CellStyle)
                                .Text(emp.FullName);

                            table.Cell()
                                .Element(CellStyle)
                                .Text(emp.Department);

                            table.Cell()
                                .Element(CellStyle)
                                .Text(emp.Designation);

                            table.Cell()
                                .Element(CellStyle)
                                .AlignRight()
                                .Text($"₹ {emp.Salary:N0}");

                            table.Cell()
                                .Element(CellStyle)
                                .Text(emp.Status);
                        }
                    });

                    page.Footer().Column(column =>
                    {
                        column.Item()
                            .LineHorizontal(1)
                            .LineColor(Colors.Grey.Lighten2);

                        column.Item()
                            .PaddingTop(5)
                            .Row(row =>
                            {
                                row.RelativeItem()
                                    .Text($"Generated on {DateTime.Now:dd MMM yyyy HH:mm}");

                                row.ConstantItem(100)
                                    .AlignRight()
                                    .Text(text =>
                                    {
                                        text.Span("Page ");
                                        text.CurrentPageNumber();
                                        text.Span(" / ");
                                        text.TotalPages();
                                    });
                            });
                    });
                });
            });

            return pdf.GeneratePdf();
        }

        public async Task<byte[]> ExportEmployeesExcelAsync()
        {
            var employees = await _repository.GetEmployeeReportAsync();

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add("Employees");

            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Department";
            worksheet.Cell(1, 3).Value = "Designation";
            worksheet.Cell(1, 4).Value = "Salary";
            worksheet.Cell(1, 5).Value = "Status";

            var header = worksheet.Range("A1:E1");

            header.Style.Font.Bold = true;
            header.Style.Fill.BackgroundColor = XLColor.LightBlue;
            header.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            int row = 2;

            foreach (var emp in employees)
            {
                worksheet.Cell(row, 1).Value = emp.FullName;
                worksheet.Cell(row, 2).Value = emp.Department;
                worksheet.Cell(row, 3).Value = emp.Designation;
                worksheet.Cell(row, 4).Value = emp.Salary;
                worksheet.Cell(row, 5).Value = emp.Status;

                row++;
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }

        public async Task<byte[]> ExportAttendancePdfAsync()
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var attendance = await _repository.GetAttendanceReportAsync();

            static IContainer CellStyle(IContainer container)
            {
                return container
                    .BorderBottom(1)
                    .BorderColor(Colors.Grey.Lighten2)
                    .PaddingVertical(8)
                    .PaddingHorizontal(6);
            }

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(25);

                    page.Header().Column(column =>
                    {
                        column.Item()
                            .Text("Employee Management System")
                            .FontSize(22)
                            .Bold()
                            .FontColor(Colors.Blue.Darken2);

                        column.Item()
                            .Text("Attendance Report")
                            .FontSize(16)
                            .SemiBold();

                        column.Item()
                            .Text($"Total Records: {attendance.Count}")
                            .FontSize(11);

                        column.Item()
                            .PaddingTop(5)
                            .LineHorizontal(1);
                    });

                    page.Content().PaddingVertical(15).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn(2);
                            columns.RelativeColumn(2);
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Element(CellStyle).Text("Employee").Bold();
                            header.Cell().Element(CellStyle).Text("Department").Bold();
                            header.Cell().Element(CellStyle).Text("Date").Bold();
                            header.Cell().Element(CellStyle).Text("Status").Bold();
                            header.Cell().Element(CellStyle).Text("Check In").Bold();
                        });

                        foreach (var item in attendance)
                        {
                            table.Cell().Element(CellStyle).Text(item.EmployeeName);
                            table.Cell().Element(CellStyle).Text(item.Department);
                            table.Cell().Element(CellStyle).Text(item.AttendanceDate.ToString("dd MMM yyyy"));
                            table.Cell().Element(CellStyle).Text(item.Status);
                            table.Cell().Element(CellStyle).Text(item.CheckIn?.ToString() ?? "-");
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Generated on {DateTime.Now:dd MMM yyyy HH:mm}");
                });
            });

            return pdf.GeneratePdf();
        }
    }
}