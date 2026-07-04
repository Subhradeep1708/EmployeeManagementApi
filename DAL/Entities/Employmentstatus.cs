using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Entities;

[Table("employmentstatuses")]
[Index("StatusName", Name = "StatusName", IsUnique = true)]
public partial class Employmentstatus
{
    [Key]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string StatusName { get; set; } = null!;

    [StringLength(255)]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [InverseProperty("Status")]
    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
