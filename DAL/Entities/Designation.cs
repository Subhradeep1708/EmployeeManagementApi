using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Entities;

[Table("designations")]
[Index("DesignationCode", Name = "DesignationCode", IsUnique = true)]
[Index("DepartmentId", Name = "FK_Designation_Department")]
public partial class Designation
{
    [Key]
    public int DesignationId { get; set; }

    [StringLength(20)]
    public string DesignationCode { get; set; } = null!;

    [StringLength(100)]
    public string DesignationName { get; set; } = null!;

    public int DepartmentId { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("DepartmentId")]
    [InverseProperty("Designations")]
    public virtual Department Department { get; set; } = null!;

    [InverseProperty("Designation")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
