using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RegisterAPI.Models;

[Index("AreaId", Name = "IX_Students_AreaId")]
[Index("AspirationId", Name = "IX_Students_AspirationId")]
[Index("HighSchoolId", Name = "IX_Students_HighSchoolId")]
[Index("MajorId", Name = "IX_Students_MajorId")]
[Index("ProvinceId", Name = "IX_Students_ProvinceId")]
public partial class Student
{
    [Key]
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; } = null!;

    [Column("CCCD")]
    public string Cccd { get; set; } = null!;

    public bool Prioritize { get; set; }

    public int ProvinceId { get; set; }

    public int HighSchoolId { get; set; }

    public int AreaId { get; set; }

    public int MajorId { get; set; }

    public int? AspirationId { get; set; }

    public bool Gender { get; set; }

    [ForeignKey("AreaId")]
    [InverseProperty("Students")]
    public virtual Area Area { get; set; } = null!;

    [ForeignKey("AspirationId")]
    [InverseProperty("Students")]
    public virtual Aspiration? Aspiration { get; set; }

    [ForeignKey("HighSchoolId")]
    [InverseProperty("Students")]
    public virtual HighSchool HighSchool { get; set; } = null!;

    [ForeignKey("MajorId")]
    [InverseProperty("Students")]
    public virtual Major Major { get; set; } = null!;

    [ForeignKey("ProvinceId")]
    [InverseProperty("Students")]
    public virtual Province Province { get; set; } = null!;
}
