using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Entities;

[Table("attendance")]
[Index("EmployeeId", "AttendanceDate", Name = "UK_Employee_Date", IsUnique = true)]
public partial class Attendance
{
    [Key]
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly AttendanceDate { get; set; }

    [Column(TypeName = "time")]
    public TimeOnly? CheckIn { get; set; }

    [Column(TypeName = "time")]
    public TimeOnly? CheckOut { get; set; }

    [Column(TypeName = "enum('Present','Absent','Leave','Half Day','WFH')")]
    public string Status { get; set; } = null!;

    [StringLength(255)]
    public string? Remarks { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Attendances")]
    public virtual Employee Employee { get; set; } = null!;
}
