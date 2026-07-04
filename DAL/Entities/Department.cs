using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Entities;

[Table("departments")]
[Index("DepartmentCode", Name = "DepartmentCode", IsUnique = true)]
[Index("DepartmentName", Name = "DepartmentName", IsUnique = true)]
public partial class Department
{
    [Key]
    public int DepartmentId { get; set; }

    [StringLength(20)]
    public string DepartmentCode { get; set; } = null!;

    [StringLength(100)]
    public string DepartmentName { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Department")]
    public virtual ICollection<Designation> Designations { get; set; } = new List<Designation>();

    [InverseProperty("Department")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
