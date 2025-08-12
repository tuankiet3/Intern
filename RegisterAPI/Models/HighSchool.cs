using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RegisterAPI.Models;

[Index("ProvinceId", Name = "IX_HighSchools_ProvinceId")]
public partial class HighSchool
{
    [Key]
    public int HighSchoolId { get; set; }

    public string? HighSchoolName { get; set; }

    public int ProvinceId { get; set; }

    [ForeignKey("ProvinceId")]
    [InverseProperty("HighSchools")]
    public virtual Province Province { get; set; } = null!;

    [InverseProperty("HighSchool")]
    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
