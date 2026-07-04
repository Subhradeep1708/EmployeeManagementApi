using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.DAL.Entities;

[Table("users")]
[Index("EmployeeId", Name = "EmployeeId", IsUnique = true)]
[Index("RoleId", Name = "FK_User_Role")]
[Index("Username", Name = "Username", IsUnique = true)]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    public int EmployeeId { get; set; }

    [StringLength(100)]
    public string Username { get; set; } = null!;

    [Column(TypeName = "text")]
    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public bool? IsActive { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastLogin { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column(TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("User")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("Users")]
    public virtual Role Role { get; set; } = null!;
}
