using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Entities;

[Table("employees")]
[Index("Email", Name = "Email", IsUnique = true)]
[Index("EmployeeCode", Name = "EmployeeCode", IsUnique = true)]
[Index("DepartmentId", Name = "FK_Employee_Department")]
[Index("DesignationId", Name = "FK_Employee_Designation")]
[Index("StatusId", Name = "FK_Employee_Status")]
public partial class Employee
{
    [Key]
    public int EmployeeId { get; set; }

    [StringLength(20)]
    public string EmployeeCode { get; set; } = null!;

    [StringLength(100)]
    public string FirstName { get; set; } = null!;

    [StringLength(100)]
    public string? LastName { get; set; }

    [Column(TypeName = "enum('Male','Female','Other')")]
    public string Gender { get; set; } = null!;

    public DateOnly? DateOfBirth { get; set; }

    [StringLength(150)]
    public string Email { get; set; } = null!;

    [StringLength(20)]
    public string? Phone { get; set; }

    [Column(TypeName = "text")]
    public string? Address { get; set; }

    public DateOnly HireDate { get; set; }

    public int DepartmentId { get; set; }

    public int DesignationId { get; set; }

    public int StatusId { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    [ForeignKey("DepartmentId")]
    [InverseProperty("Employees")]
    public virtual Department Department { get; set; } = null!;

    [ForeignKey("DesignationId")]
    [InverseProperty("Employees")]
    public virtual Designation Designation { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual ICollection<Performance> Performances { get; set; } = new List<Performance>();

    [InverseProperty("Employee")]
    public virtual ICollection<Salaryhistory> Salaryhistories { get; set; } = new List<Salaryhistory>();

    [ForeignKey("StatusId")]
    [InverseProperty("Employees")]
    public virtual Employmentstatus Status { get; set; } = null!;

    [InverseProperty("Employee")]
    public virtual User? User { get; set; }
}
