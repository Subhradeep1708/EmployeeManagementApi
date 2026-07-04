using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Entities;

[Table("performance")]
[Index("EmployeeId", Name = "FK_Performance_Employee")]
public partial class Performance
{
    [Key]
    public int PerformanceId { get; set; }

    public int EmployeeId { get; set; }

    public DateOnly ReviewDate { get; set; }

    [Precision(3, 2)]
    public decimal Rating { get; set; }

    public int? TasksCompleted { get; set; }

    [Column(TypeName = "text")]
    public string? Remarks { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Performances")]
    public virtual Employee Employee { get; set; } = null!;
}
