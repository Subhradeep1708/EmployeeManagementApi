using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Entities;

[Table("salaryhistory")]
[Index("EmployeeId", Name = "FK_Salary_Employee")]
public partial class Salaryhistory
{
    [Key]
    public int SalaryId { get; set; }

    public int EmployeeId { get; set; }

    [Precision(12, 2)]
    public decimal BasicSalary { get; set; }

    [Precision(12, 2)]
    public decimal? Bonus { get; set; }

    [Precision(12, 2)]
    public decimal? Deduction { get; set; }

    [Precision(12, 2)]
    public decimal? NetSalary { get; set; }

    public DateOnly EffectiveFrom { get; set; }

    public DateOnly? EffectiveTo { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("Salaryhistories")]
    public virtual Employee Employee { get; set; } = null!;
}
